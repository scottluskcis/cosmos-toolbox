using CosmosToolbox.Core.Options;

namespace CosmosToolbox.Core.Data
{
    public interface IClientContextFactory
    {
        IClientContext Create(ClientContextOptions options);
    }
}
