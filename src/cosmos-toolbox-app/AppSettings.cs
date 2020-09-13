using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CosmosToolbox.App
{
    public class AppSettings
    {
        public static Lazy<JsonSerializerSettings> JsonSerializerSettings = 
            new Lazy<JsonSerializerSettings>(GetDefaultSerializerSettings);

        private static JsonSerializerSettings GetDefaultSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };
        }
    }
}