using System.Threading.Tasks;
using CosmosToolbox.App.Options;
using Microsoft.Extensions.Logging;

namespace CosmosToolbox.App.Strategy
{
    internal class InitOptionsStrategy : IAppStrategy
    {
        private readonly IOptionsService _service;
        private readonly ILogger _logger;

        public InitOptionsStrategy(IOptionsService service, ILogger<InitOptionsStrategy> logger)
        {
            _service = service;
            _logger = logger;
        }

        public int Order => 1;

        public bool IsApplicable(IApplicationArgs args)
        {
            return true; // TODO: add the logic to check the arg
        }

        public async Task RunAsync(IApplicationArgs args)
        {
            _logger.LogDebug($"{nameof(InitOptionsStrategy)}.{nameof(RunAsync)} called");
            await _service.CreateOptionsFileAsync();
        }
    }
}