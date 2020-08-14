using System;
using System.Threading.Tasks;
using CosmosToolbox.App;

namespace CosmosToolbox
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // TODO: setup using DI
            var app = new Application();
            await app.RunAsync(args);
        }
    }
}
