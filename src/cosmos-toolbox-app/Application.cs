using System.Threading.Tasks;

namespace CosmosToolbox.App
{
    public interface ICosmosToolboxApplication
    {
        Task RunAsync(string[] args);
    }

    public class Application : ICosmosToolboxApplication
    {
        public Task RunAsync(string[] args)
        {
            throw new System.NotImplementedException();
        }
    }
}