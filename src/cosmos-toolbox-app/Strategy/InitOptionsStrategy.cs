using System.Threading.Tasks;
using CosmosToolbox.App.Options;

namespace CosmosToolbox.App.Strategy
{
    internal class InitOptionsStrategy : IAppStrategy
    {
        private readonly IOptionsService _service;

        public InitOptionsStrategy()
        {
            // TODO: inject this via DI
            _service = new OptionsService();
        }

        public int Order => 1;

        public bool IsApplicable(string arg)
        {
            return true; // TODO: add the logic to check the arg
        }

        public async Task RunAsync(string[] args)
        {
            await _service.CreateOptionsFileAsync();
        }
    }
}