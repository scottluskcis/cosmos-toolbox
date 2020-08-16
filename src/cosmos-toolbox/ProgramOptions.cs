using System.Text;
using CommandLine;
using CosmosToolbox.App;

namespace CosmosToolbox
{
    public class ProgramOptions : IApplicationArgs
    {
        [Option('a', "action", Required = false, Default = "validate", HelpText = "Optional. Specify Analysis, DataSetToXml, or Validate, defaults to Validate if not specified")]
        public string Action { get; set; }
        
        [Option('e', "error-status-code", Required = false, Default = -1, HelpText = "Exit status code to assign to Environment.ExitCode if an Exception occurs, defaults to -1")]
        public int ErrorStatusCode { get; set; }

        [Option('p', "pause-before-exit", Default = false, Required = false, HelpText = "True to have console pause awaiting Enter key to be pressed before exiting")]
        public bool PauseBeforeExit { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{nameof(ProgramOptions)} Parsed:");
            sb.AppendLine("--------------------------------------------------------------------");
            sb.AppendLine($"  -a, --action {Action}");
            sb.AppendLine($"  -e, --error-status-code {ErrorStatusCode}");
            sb.AppendLine($"  -p, --pause-before-exit {PauseBeforeExit}");
            sb.AppendLine("--------------------------------------------------------------------");
             
            return sb.ToString();
        }
    }
}