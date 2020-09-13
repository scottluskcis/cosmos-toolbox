using System;
using CosmosToolbox.Core.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CosmosToolbox.Core.Converters
{
    public sealed class EnumerationJsonConverter : JsonConverter
    {
        public override bool CanRead => true;

        public override bool CanWrite => true;

        public bool NameIsNullable { get; set;  }

        public EnumerationJsonConverter()
        {
            NameIsNullable = false;
        }

        public EnumerationJsonConverter(bool nameIsNullable)
        {
            NameIsNullable = nameIsNullable;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                if(!(value is Enumeration enumeration))
                    throw new JsonSerializationException($"unable to cast type {value.GetType().Name} to type {nameof(Enumeration)}");

                if(!NameIsNullable && enumeration.Name == null)
                    throw new JsonSerializationException($"{nameof(Enumeration.Name)} cannot be null");

                if (enumeration.Name != null)
                    writer.WriteValue(enumeration.Name);
                else
                    writer.WriteValue(enumeration.Id);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null || reader.TokenType == JsonToken.None || reader.TokenType == JsonToken.Undefined)
            {
                return null;
            }

            string str = null;
            if (reader.TokenType == JsonToken.StartObject)
            {
                var jObject = JObject.Load(reader);
                if (jObject.TryGetValue(nameof(Enumeration.Name), StringComparison.OrdinalIgnoreCase, out var nameValue))
                    str = nameValue?.ToString();
                
                if (string.IsNullOrEmpty(str) && jObject.TryGetValue(nameof(Enumeration.Id), StringComparison.OrdinalIgnoreCase, out var idValue))
                    str = idValue?.ToString();
            }
            else
            {
                str = reader.Value?.ToString();
            }

            if (!string.IsNullOrEmpty(str) && Enumeration.TryParse(objectType, str, out var enumeration))
            {
                return enumeration;
            }

            throw new JsonSerializationException($"unable to parse value '{str}' for type {objectType.Name}");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Enumeration)
                   || typeof(Enumeration).IsAssignableFrom(objectType)
                   || objectType == typeof(string)
                   || objectType == typeof(int);
        }
    }
}
