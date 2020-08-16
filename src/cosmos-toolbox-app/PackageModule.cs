using CosmosToolbox.App.Data;
using CosmosToolbox.App.Options;
using CosmosToolbox.App.Strategy;
using CosmosToolbox.Core.Data;
using CosmosToolbox.Core.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosToolbox.App
{
    public class PackageModule : IPackageModule
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IClientContext, CosmosClientContext>();
            
            services.AddTransient<IOptionsService, OptionsService>();
            services.AddTransient<IAppStrategy, CreateContainersStrategy>();
            services.AddTransient<IAppStrategy, ValidateOptionsStrategy>();
            services.AddSingleton<ICosmosToolboxApplication, Application>();
        }
    }
}