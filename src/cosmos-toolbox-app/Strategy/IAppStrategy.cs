using System.Threading.Tasks;

namespace CosmosToolbox.App.Strategy
{
    public interface IAppStrategy
    {
        int Order { get; }
        
        bool IsApplicable(string arg);

        Task RunAsync(string[] args);
    }
}