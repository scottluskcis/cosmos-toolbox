using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CosmosToolbox.Core.IoC;
using CosmosToolbox.Core.Options;

namespace CosmosToolbox
{
    internal sealed class AppServicesProvider : IDisposable
    {
        private readonly static Lazy<AppServicesProvider> _instance 
            = new Lazy<AppServicesProvider>(() => new AppServicesProvider());

        public static AppServicesProvider Instance 
        {
            get => _instance.Value;
        }
        
        private AppServicesProvider()
        {
            BuildConfiguration();
            RegisterServices();
        }

        private IConfigurationRoot _configuration;
        private IServiceProvider _serviceProvider;

        public TService GetRequiredService<TService>()
        {
            return _serviceProvider.GetRequiredService<TService>();
        }

        private void BuildConfiguration()
        {
            var environmentName = Environment.GetEnvironmentVariable("ENVIRONMENT");

            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", false)
               .AddJsonFile($"appsettings.{environmentName}.json", true)
               .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        private void RegisterServices()
        {
            var collection = new ServiceCollection();

            collection.AddLogging((logging) =>
            {
                logging.AddConsole();
            });

            collection.AddOptions<ClientContextOptions>()
                .Bind(_configuration.GetSection("CosmosDb"));

            var modules = new IPackageModule[]
            {
                new CosmosToolbox.Core.PackageModule(),
                new CosmosToolbox.App.PackageModule()
            };

            foreach(var module in modules)
                module.RegisterServices(collection);
                
            _serviceProvider = collection.BuildServiceProvider();
        }

        private void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }

        public void Dispose()
        {
            DisposeServices();
            
            _serviceProvider = null;
            _configuration = null;
        }
    }
}