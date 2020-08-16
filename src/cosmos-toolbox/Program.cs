using System;
using System.Threading.Tasks;
using CommandLine;
using CosmosToolbox.App;

namespace CosmosToolbox
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<ProgramOptions>(args)
                .MapResult(RunAsync, _ => Task.FromResult(1));
        }

        static async Task<int> RunAsync(ProgramOptions opts)
        {
            int exitCode = 0;
            try
            {
                Console.WriteLine(opts.ToString());

                var service = AppServicesProvider.Instance.GetRequiredService<ICosmosToolboxApplication>();
                await service.RunAsync(opts);

                if (opts.PauseBeforeExit)
                    Console.ReadKey();
            }
            catch (Exception ex)
            {
                exitCode = opts.ErrorStatusCode;
                Console.WriteLine(ex.ToString());

                if (opts.PauseBeforeExit)
                    Console.ReadKey();
            }
            finally
            {
                AppServicesProvider.Instance.Dispose();
                Environment.ExitCode = exitCode;
            }
            return exitCode;
        }
    }
}
