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

            if (!(context is CosmosClientContext cosmosClientContext))
                throw new NotSupportedException($"Context must be of type {nameof(CosmosClientContext)} to perform this operation");

            if (args.CreateDatabase)
            {
                _logger.LogInformation($"creating database {options.Database.Id}");
                await cosmosClientContext.CreateDatabaseIfNotExistsAsync(options.Database.Id, options.Database.Throughput);
            }

            if(args.CreateContainers)
            {
                _logger.LogInformation("creating containers");
                
                var containerTasks = options
                    .Database.Containers
                    .Select(c => cosmosClientContext.CreateContainerIfNotExistsAsync(c.Id, c.PartitionKey));

                await Task.WhenAll(containerTasks);
            }
        }
    }
}