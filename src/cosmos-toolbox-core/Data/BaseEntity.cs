using Newtonsoft.Json;
using CosmosToolbox.Core.Extensions;

namespace CosmosToolbox.Core.Data
{
    public abstract class BaseEntity : IEntity
    {
        public abstract string GetId();

        [JsonProperty("docType")]
        public string Type => this.GetType().Name;

        [JsonProperty("partitionKey")]
        public string PartitionKey => this.GetPartitionKeyValue();
    }
}