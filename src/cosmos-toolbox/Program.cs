using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CosmosToolbox.App;
using System.IO;
using CosmosToolbox.Core.IoC;

namespace CosmosToolbox
{
    class Program
    {
        private static IConfigurationRoot _configuration;
        private static IServiceProvider _serviceProvider;

        static async Task Main(string[] args)
        {
            try
            {
                BuildConfiguration();
                RegisterServices();

                var service = _serviceProvider.GetRequiredService<ICosmosToolboxApplication>();
                await service.RunAsync(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                DisposeServices();
            }

            Console.ReadKey();
        }

        private static void BuildConfiguration()
        {
            var environmentName = Environment.GetEnvironmentVariable("ENVIRONMENT");

            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", false)
               .AddJsonFile($"appsettings.{environmentName}.json", true)
               .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        private static void RegisterServices()
        {
            var collection = new ServiceCollection();

            collection.AddLogging((logging) =>
            {
                logging.AddConsole();
            });

            var modules = new IPackageModule[]
            {
                new CosmosToolbox.Core.PackageModule(),
                new CosmosToolbox.App.PackageModule()
            };

            foreach(var module in modules)
                module.RegisterServices(collection);
                
            _serviceProvider = collection.BuildServiceProvider();
        }

        private static void DisposeServices()
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
    }
}
