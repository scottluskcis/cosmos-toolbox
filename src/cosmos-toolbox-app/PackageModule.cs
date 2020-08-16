using CosmosToolbox.App.Options;
using CosmosToolbox.App.Strategy;
using CosmosToolbox.Core.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosToolbox.App
{
    public class PackageModule : IPackageModule
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IOptionsService, OptionsService>();
            services.AddTransient<IAppStrategy, InitOptionsStrategy>();
            services.AddSingleton<ICosmosToolboxApplication, Application>();
        }
    }
}