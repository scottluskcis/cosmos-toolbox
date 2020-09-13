using System;
using CosmosToolbox.Core.Options;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace CosmosToolbox.App.Extensions
{
    public static class OptionsExtensions
    {
        public static CosmosClient CreateCosmosClient(this ClientContextOptions options)
        {
            options.Validate();

            var clientBuilder = new CosmosClientBuilder(
                options.ConnectionString);
                        
            if (options.UseThrottling) 
                clientBuilder = clientBuilder
                    .WithThrottlingRetryOptions(
                        options.MaxRetryWaitTimeOnThrottledRequests ?? new TimeSpan(0, 0, 0, 30),
                        options.MaxRetryAttemptsOnThrottledRequests ?? 3);

            if (Enum.TryParse<ConsistencyLevel>(options.ConsistencyLevel, out var consistencyLevel))
                clientBuilder = clientBuilder
                    .WithConsistencyLevel(consistencyLevel);

            if (options.AllowBulkExecution.HasValue)
                clientBuilder = clientBuilder
                    .WithBulkExecution(options.AllowBulkExecution.Value);

            if(options.CamelCasePropertyNames)
                clientBuilder = clientBuilder.WithSerializerOptions(new CosmosSerializationOptions
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                });

            return clientBuilder.Build();
        }

        public static void Validate(this ClientContextOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrEmpty(options.ConnectionString))
                throw new ArgumentNullException(nameof(options.ConnectionString));

            if (options.Database == null)
                throw new ArgumentNullException(nameof(options.Database));

            options.Database.Validate();
        }
    }
}