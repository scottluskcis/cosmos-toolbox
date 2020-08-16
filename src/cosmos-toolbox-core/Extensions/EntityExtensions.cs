using System;
using System.Linq;
using CosmosToolbox.Core.Data;
using CosmosToolbox.Core.Attributes;

namespace CosmosToolbox.Core.Extensions
{
    public static class EntityExtensions
    {
        public static string GetPartitionKeyValue(this BaseEntity entity)
        {
            var type = entity.GetType();
            
            var attribute = type
                .GetCustomAttributes(typeof(CosmosEntityAttribute), true)
                .OfType<CosmosEntityAttribute>()
                .Single();

            if(!attribute.PartitionKeyProperties.Any())
                throw new ArgumentException($"At least one property must be indicated in {nameof(CosmosEntityAttribute.PartitionKeyProperties)}");
             
            var values = attribute.PartitionKeyProperties
                .Select(propertyName => 
                    type.GetProperty(propertyName)?.GetValue(entity))
                .Where(s => s != null)
                .ToList();

            if(values.Count != attribute.PartitionKeyProperties.Length)
                throw new NullReferenceException($"PartitionKey could not be determined from indicated properties: {string.Join(',', attribute.PartitionKeyProperties)}");

            var partitionKeyValue = string.Join(attribute.PartitionKeyPropertySeparator, values);
            return partitionKeyValue;
        }
    }
}