using System;
using System.Collections.Generic;

namespace CosmosToolbox.Core.Options
{
    public sealed class ClientContextOptionsGroup
    {
        public IEnumerable<ClientContextOptions> Databases { get; set; }
    }

    public sealed class ClientContextOptions
    {
        public DatabaseOptions Database { get; set; }
        public string ConnectionString { get; set; }
        public string ConsistencyLevel { get; set; }
        public bool? AllowBulkExecution { get; set; }
        public bool CamelCasePropertyNames { get; set; }
        
        public bool UseThrottling =>
            MaxRetryAttemptsOnThrottledRequests.HasValue ||
            MaxRetryWaitTimeOnThrottledRequests.HasValue;

        public TimeSpan? MaxRetryWaitTimeOnThrottledRequests { get; set; } 
        public int? MaxRetryAttemptsOnThrottledRequests { get; set; }

        public string GetContextIdentifier()
        {
            return $"{Database?.Api}_{Database?.Id}";
        }
    }
}