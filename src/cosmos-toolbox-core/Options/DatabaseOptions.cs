using System.Collections.Generic;

namespace CosmosToolbox.Core.Options
{
    /// <summary>
    /// options used to configure a Cosmos Database
    /// </summary>
    public class DatabaseOptions
    {
        /// <summary>
        /// Database Id
        /// </summary>
        /// <value></value>
        public string Id { get; set; }

        /// <summary>
        /// specify a value to set shared throughput at datbase level
        /// </summary>
        /// <value></value>
        public int? Throughput { get; set; }

        /// <summary>
        /// any containers to be created within this database
        /// </summary>
        /// <value></value>
        public IEnumerable<ContainerOptions> Containers { get; set; }
    }
}