using CosmosToolbox.Core.Converters;
using Newtonsoft.Json;

namespace CosmosToolbox.Core.Enums
{
    /// <summary>
    /// supported database APIs
    /// </summary>
    [JsonConverter(typeof(EnumerationJsonConverter))]
    public sealed class DatabaseApi : Enumeration
    {
        public static readonly DatabaseApi SqlApi = new DatabaseApi(1, "sql");
        public static readonly DatabaseApi MongoDbApi = new DatabaseApi(2, "mongodb");
        public static readonly DatabaseApi TableApi = new DatabaseApi(3, "table");
        public static readonly DatabaseApi CassandraApi = new DatabaseApi(4, "cassandra");
        public static readonly DatabaseApi GremlinApi = new DatabaseApi(5, "gremlin");

        private DatabaseApi(int id, string name) 
            : base(id, name) { }
    }
}
