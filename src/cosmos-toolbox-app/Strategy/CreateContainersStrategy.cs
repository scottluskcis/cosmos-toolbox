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
        private readonly ClientContextOptions _options;
        private readonly IClientContext _context;
        private readonly ILogger _logger;

        public CreateContainersStrategy(IOptions<ClientContextOptions> options, IClientContext context, ILogger<CreateContainersStrategy> logger)
        {
            _options = options?.Value;
            _context = context;
            _logger = logger;
        }

        public int Order => 2;

        public bool IsApplicable(IApplicationArgs args)
        {
            return args.CreateDatabase || args.CreateContainers;
        }

        public async Task RunAsync(IApplicationArgs args)
        {
            _options.Validate();

            if (!(_context is CosmosClientContext cosmosClientContext))
                throw new NotSupportedException($"Context must be of type {nameof(CosmosClientContext)} to perform this operation");

            if (args.CreateDatabase)
            {
                _logger.LogInformation($"creating database {_options.Database.Id}");
                await cosmosClientContext.CreateDatabaseIfNotExistsAsync(_options.Database.Id, _options.Database.Throughput);
            }

            if(args.CreateContainers)
            {
                _logger.LogInformation("creating containers");
                
                var containerTasks = _options
                    .Database.Containers
                    .Select(c => cosmosClientContext.CreateContainerIfNotExistsAsync(c.Id, c.PartitionKey));

                await Task.WhenAll(containerTasks);
            }
        }
    }
}