namespace CosmosToolbox.Core.Options
{
    /// <summary>
    /// options used to configure a Cosmos Database, Containers, and related Objects
    /// </summary>
    public class CosmosToolboxOptions
    {
        /// <summary>
        /// options for database
        /// </summary>
        /// <value></value>
        public DatabaseOptions Database { get; set; }
    }
}