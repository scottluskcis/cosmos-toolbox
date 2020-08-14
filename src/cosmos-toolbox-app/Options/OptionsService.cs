using CosmosToolbox.Core.Options;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace CosmosToolbox.App.Options
{
    public class OptionsConstants
    {
        public const string OptionsFileName = "cosmos-toolbox-options.json";
    }

    /// <summary>
    /// service for managing <see cref="CosmosToolboxOptions" />
    /// </summary>
    public interface IOptionsService
    {
        /// <summary>
        /// initializes an empty options file
        /// </summary>
        /// <param name="path">location to save the file to</param>
        Task CreateOptionsFileAsync(string path = "");
        
        /// <summary>
        /// creates options that can be used to save as template
        /// </summary>
        /// <returns></returns>
        CosmosToolboxOptions CreateEmptyOptions();
    }

    public class OptionsService : IOptionsService
    {
        public async Task CreateOptionsFileAsync(string path = "")
        {
            var options = CreateEmptyOptions();
            var filePath = Path.Combine(path, OptionsConstants.OptionsFileName);
            
            var json = JsonConvert.SerializeObject(options, AppSettings.JsonSerializerSettings.Value);
            await File.WriteAllTextAsync(filePath, json);
        }

        public CosmosToolboxOptions CreateEmptyOptions()
        {
            var options = new CosmosToolboxOptions
            {
                Database = new DatabaseOptions
                {
                    Id = "<Database Id>",
                    Throughput = 400,
                    Containers = new [] 
                    {
                        new ContainerOptions
                        {
                            Id = "<Container Id>",
                            Throughput = null,
                            PartitionKey = "<(e.g. /addres/zipCode)>"
                        }
                    }
                }
            };

            return options;
        }

    }
}