using System.Threading.Tasks;
using CosmosToolbox.App.Extensions;
using CosmosToolbox.Core.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CosmosToolbox.App.Strategy
{
    public sealed class ValidateOptionsStrategy : IAppStrategy
    {
        private readonly ClientContextOptions _options;
        private readonly ILogger _logger;

        public ValidateOptionsStrategy(IOptions<ClientContextOptions> options, ILogger<ValidateOptionsStrategy> logger)
        {
            _options = options?.Value;
            _logger = logger;
        }

        public int Order => 1;

        public bool IsApplicable(IApplicationArgs args)
        {
            return args.OnlyValidate;
        }

        public Task RunAsync(IApplicationArgs args)
        {
            _options.Validate();
            return Task.FromResult(true);
        }
    }
}