using Microsoft.Extensions.DependencyInjection;

namespace CosmosToolbox.Core.IoC
{
    public interface IPackageModule
    {
         void RegisterServices(IServiceCollection services);
    }
}