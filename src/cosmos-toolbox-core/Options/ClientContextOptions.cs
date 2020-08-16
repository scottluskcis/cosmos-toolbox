using System;

namespace CosmosToolbox.Core.Options
{
    public sealed class ClientContextOptions
    {
        public DatabaseOptions Database { get; set; }
        public string EndpointUri { get; set; }
        public string PrimaryKey { get; set; }
        public string ConsistencyLevel { get; set; }
        public bool? AllowBulkExecution { get; set; }
        public bool CamelCasePropertyNames { get; set; }
        
        public bool UseThrottling =>
            MaxRetryAttemptsOnThrottledRequests.HasValue ||
            MaxRetryWaitTimeOnThrottledRequests.HasValue;

        public TimeSpan? MaxRetryWaitTimeOnThrottledRequests { get; set; } 
        public int? MaxRetryAttemptsOnThrottledRequests { get; set; }
    }
}