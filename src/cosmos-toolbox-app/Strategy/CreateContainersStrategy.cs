using System;
using System.Linq;
using System.Threading.Tasks;
using CosmosToolbox.App.Data;
using CosmosToolbox.App.Extensions;
using CosmosToolbox.Core.Data;
using CosmosToolbox.Core.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CosmosToolbox.App.Strategy
{
    public sealed class CreateContainersStrategy : IAppStrategy
    {
        private readonly ClientContextOptionsGroup _options;
        private readonly IClientContextFactory _factory;
        private readonly ILogger _logger;

        public CreateContainersStrategy(IOptions<ClientContextOptionsGroup> options, IClientContextFactory factory, ILogger<CreateContainersStrategy> logger)
        {
            _options = options?.Value;
            _factory = factory;
            _logger = logger;
        }

        public int Order => 2;

        public bool IsApplicable(IApplicationArgs args)
        {
            return args.CreateDatabase || args.CreateContainers;
        }

        public async Task RunAsync(IApplicationArgs args)
        {
            foreach (var options in _options.Databases)
                await RunAsync(options, args);
        }

        private async Task RunAsync(ClientContextOptions options, IApplicationArgs args)
        { 
            options.Validate();

            var context = _factory.Create(options);

            if (context is CosmosSqlApiClientContext sqlApiClientContext)
            {
                if (args.CreateDatabase)
                {
                    _logger.LogInformation($"creating database {options.Database.Id}");
                    await sqlApiClientContext.CreateDatabaseIfNotExistsAsync(options.Database.Id,
                        options.Database.Throughput);
                }

                if (args.CreateContainers)
                {
                    _logger.LogInformation("creating containers");

                    var containerTasks = options
                        .Database.Containers
                        .Select(container => sqlApiClientContext.CreateContainerIfNotExistsAsync(container.Id, container.PartitionKey));

                    await Task.WhenAll(containerTasks);
                }
            }
            else if (context is CosmosTableApiClientContext tableApiClientContext)
            {
                if (args.CreateContainers)
                {
                    _logger.LogInformation("creating tables");

                    var tableTasks = options
                        .Database.Containers
                        .Select(table => tableApiClientContext.CreateTableIfNotExistsAsync(table.Id, table.Throughput));

                    await Task.WhenAll(tableTasks);
                }
            }
            else
            {
                throw new NotSupportedException($"no valid client context could be found to perform this operation");
            }
        }
    }
}