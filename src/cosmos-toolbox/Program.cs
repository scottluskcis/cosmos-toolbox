using System;
using System.Threading.Tasks;
using CosmosToolbox.App;

namespace CosmosToolbox
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var service = AppServicesProvider.Instance.GetRequiredService<ICosmosToolboxApplication>();
                await service.RunAsync(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                AppServicesProvider.Instance.Dispose();
            }

            Console.ReadKey();
        }
    }
}
