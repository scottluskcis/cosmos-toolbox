using System.Threading.Tasks;
using CosmosToolbox.App.Extensions;
using CosmosToolbox.Core.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CosmosToolbox.App.Strategy
{
    public sealed class ValidateOptionsStrategy : IAppStrategy
    {
        private readonly ClientContextOptionsGroup _options;
        private readonly ILogger _logger;

        public ValidateOptionsStrategy(IOptions<ClientContextOptionsGroup> options, ILogger<ValidateOptionsStrategy> logger)
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
            foreach (var options in _options.Databases)
            {
                _logger.LogDebug($"validating options for database {options?.Database.Id}");
                options.Validate();
                _logger.LogDebug("options are valid");
            }

            return Task.FromResult(true);
        }
    }
}