using System;
using System.Linq;
using Microsoft.Azure.Cosmos;
using CosmosToolbox.Core.Attributes;
using CosmosToolbox.Core.Data;
using CosmosToolbox.Core.Extensions;

namespace CosmosToolbox.App.Extensions
{
    public static class EntityExtensions
    {
        public static ContainerProperties GetContainerProperties(this Type type)
        {
            var attribute = type
                .GetCustomAttributes(typeof(CosmosEntityAttribute), true)
                .OfType<CosmosEntityAttribute>()
                .Single();

            var properties = new ContainerProperties(
                attribute.ContainerId, 
                attribute.PartitionKeyPath);
            
            return properties;
        }

        public static PartitionKey GetPartitionKey(this IEntity entity)
        {
            var value = entity.GetPartitionKeyValue();
            var partitionKey = value != null ? new PartitionKey(value) : PartitionKey.Null;
            return partitionKey;
        }

        public static string GetTableName(this Type type)
        {
            var attribute = type
                .GetCustomAttributes(typeof(TableEntityAttribute), true)
                .OfType<TableEntityAttribute>()
                .Single();

            return attribute.TableName;
        }
    }
}