using System.Threading.Tasks;

namespace CosmosToolbox.App.Strategy
{
    public interface IAppStrategy
    {
        int Order { get; }
        
        bool IsApplicable(IApplicationArgs args);

        Task RunAsync(IApplicationArgs args);
    }
}