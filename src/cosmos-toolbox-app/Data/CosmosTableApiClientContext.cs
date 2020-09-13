using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CosmosToolbox.App.Extensions;
using CosmosToolbox.Core.Data;
using CosmosToolbox.Core.Options;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Logging;

namespace CosmosToolbox.App.Data
{
    public sealed class CosmosTableApiClientContext : IClientContext, IDisposable
    {
        private readonly ClientContextOptions _options;  
        private readonly ILogger _logger;

        public CosmosTableApiClientContext(ClientContextOptions options, ILogger<CosmosTableApiClientContext> logger)
        {
            _options = options;
            _logger = logger;
        }

        public async Task<TEntity> CreateItemAsync<TEntity>(TEntity item, CancellationToken cancellationToken = default) 
            where TEntity : BaseEntity
        {
            if (!(item is ITableEntity tableEntity))
                throw new InvalidOperationException($"specified item must implement interface '{nameof(ITableEntity)}'");

            var operation = TableOperation.InsertOrMerge(tableEntity);
            var result = await ExecuteAsync<TEntity>(operation, cancellationToken);
            return result;
        }
        
        public async Task<TEntity> ReadItemAsync<TEntity>(string id, string partitionKey, CancellationToken cancellationToken = default) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> ReplaceItemAsync<TEntity>(TEntity item, CancellationToken cancellationToken = default) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> UpsertItemAsync<TEntity>(TEntity item, CancellationToken cancellationToken = default) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> DeleteItemAsync<TEntity>(string id, string partitionKey, CancellationToken cancellationToken = default) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> ReadItemsAsync<TEntity>(Expression<Func<TEntity, bool>> predicate = null, string partitionKey = "",
            CancellationToken cancellationToken = default) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> QueryItemsAsync<TEntity>(string sql, string partitionKey = "", CancellationToken cancellationToken = default) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        private async Task<TEntity> ExecuteAsync<TEntity>(TableOperation operation, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
        {
            var tableName = GetTableName<TEntity>();
            var operationName = operation.OperationType.ToString();

            _logger.LogInformation($"operation {operationName} for table {tableName} - Start");
            
            try
            {
                var table = GetCloudTable(tableName);

                var tableResult = await table.ExecuteAsync(operation, cancellationToken);
            
                _logger.LogTableResultInformation(tableResult, $"operation {operationName} for table {tableName} - End", operationName);

                var result = tableResult.Result as TEntity;
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"error trying to perform operation {operationName} for table {tableName}");
                throw;
            }
        }

        
        public async Task<CloudTable> CreateTableIfNotExistsAsync(string tableName, int? throughput, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("{Action} - Start", nameof(CreateTableIfNotExistsAsync));

            var table = GetCloudTable(tableName);

            _logger.LogDebug($"creating table '{tableName}'");
            var created = await table.CreateIfNotExistsAsync(cancellationToken);
            _logger.LogInformation($"table '{tableName}' created: '{created}'");
            
            _logger.LogInformation("{Action} - End", nameof(CreateTableIfNotExistsAsync));

            return table;
        }

        private string GetTableName<TEntity>()
            where TEntity : BaseEntity
        {
            return typeof(TEntity).GetTableName();
        }

        private CloudTable GetCloudTable(string tableName)
        {
            var table = GetClient().GetTableReference(tableName);
            return table;
        }

        private CloudTableClient _client;

        private CloudTableClient GetClient()
        {
            if (_client != null) return _client;
            
            _logger.LogDebug($"creating {nameof(CloudTableClient)}");
            
            var cloudStorageAccount = CloudStorageAccount.Parse(_options.ConnectionString);
            _client = cloudStorageAccount.CreateCloudTableClient() ?? throw new InvalidOperationException($"failed to successfully create a {nameof(CloudTableClient)}");
            
            _logger.LogDebug($"created {nameof(CloudTableClient)} successfully");

            return _client;
        }

        public void Dispose()
        {
            _client = null;
        }
    }
}
