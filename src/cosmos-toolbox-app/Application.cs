using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CosmosToolbox.App.Strategy;

namespace CosmosToolbox.App
{
    public interface ICosmosToolboxApplication
    {
        Task RunAsync(string[] args);
    }

    public class Application : ICosmosToolboxApplication
    {
        private readonly IEnumerable<IAppStrategy> _strategies;

        public Application(IEnumerable<IAppStrategy> strategies)
        {
            _strategies = strategies;
        }

        public async Task RunAsync(string[] args)
        {
            var strategiesToRun = _strategies
                .Where(p => p.IsApplicable("some arg"))
                .OrderBy(o => o.Order)
                .Select(s => s.RunAsync(args));
            
            await Task.WhenAll(strategiesToRun);
        }
    }
}