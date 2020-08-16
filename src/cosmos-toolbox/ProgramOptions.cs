using System.Text;
using CommandLine;
using CosmosToolbox.App;

namespace CosmosToolbox
{
    public class ProgramOptions : IApplicationArgs
    {
        [Option('e', "error-status-code", Required = false, Default = -1, HelpText = "Exit status code to assign to Environment.ExitCode if an Exception occurs, defaults to -1")]
        public int ErrorStatusCode { get; set; }

        [Option('p', "pause-before-exit", Default = false, Required = false, HelpText = "True to have console pause awaiting Enter key to be pressed before exiting")]
        public bool PauseBeforeExit { get; set; }

        [Option('v', "validation-only", Required = false, Default = true, HelpText = "True to perform validation of configuration only")]
        public bool OnlyValidate { get; set; }

        [Option('d', "create-database", Required = false, Default = false, HelpText = "True to create database specified in configuration")]
        public bool CreateDatabase { get; set; }

        [Option('c', "create-containers", Required = false, Default = false, HelpText = "True to create any containers specified in configuration")]
        public bool CreateContainers { get; set; }

        [Option('l', "load-seed-data", Required = false, Default = false, HelpText = "True to load any seed data for containers specified in configuration")]
        public bool LoadSeedData { get; set; }

        [Option('s', "load-scripts", Required = false, Default = false, HelpText = "True to load any server side scripts")]
        public bool LoadScripts { get; set; }

        [Option('a', "configure-all", Required = false, Default = false, HelpText = "True to configure everything based on what's in confgiruation, overrides all other settings")]
        public bool ConfigureAll { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{nameof(ProgramOptions)} Provided:");
            sb.AppendLine("--------------------------------------------------------------------");
            sb.AppendLine($"  -v, --validation-only {OnlyValidate}");
            sb.AppendLine($"  -d, --create-database {CreateDatabase}");
            sb.AppendLine($"  -c, --create-containers {CreateContainers}");
            sb.AppendLine($"  -l, --load-seed-data {LoadSeedData}");
            sb.AppendLine($"  -s, --load-scripts {LoadScripts}");
            sb.AppendLine($"  -a, --configure-all {ConfigureAll}");
            sb.AppendLine($"  -e, --error-status-code {ErrorStatusCode}");
            sb.AppendLine($"  -p, --pause-before-exit {PauseBeforeExit}");
            sb.AppendLine("--------------------------------------------------------------------");
             
            return sb.ToString();
        }
    }
}