namespace CosmosToolbox.Core.Options
{
    /// <summary>
    /// options used to configure a Cosmos Container
    /// </summary>
    public sealed class ContainerOptions
    {
        /// <summary>
        /// container id
        /// </summary>
        /// <value></value>
        public string Id { get; set; }

        /// <summary>
        /// throughput to use for the container, if using shared throughput
        /// at the database level do not specify a Throughput for the Container
        /// unless not sharing what is set at database level
        /// </summary>
        /// <value></value>
        public int? Throughput { get; set; }

        /// <summary>
        /// partition key for the container
        /// </summary>
        /// <value></value>
        public string PartitionKey { get; set; }
    }
}