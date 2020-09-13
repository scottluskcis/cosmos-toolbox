using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CosmosToolbox.Core.Data;
using CosmosToolbox.Core.Options;
using Microsoft.Extensions.Logging;

namespace CosmosToolbox.App.Data
{
    public sealed class CosmosTableApiClientContext : IClientContext, IBulkExecutorClientContext, IDisposable
    {
        private readonly ClientContextOptions _options;  
        private readonly ILogger _logger;

        public CosmosTableApiClientContext(ClientContextOptions options, ILogger<CosmosTableApiClientContext> logger)
        {
            _options = options;
            _logger = logger;
        }

        public async Task<TEntity> CreateItemAsync<TEntity>(TEntity item, CancellationToken cancellationToken = default) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
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

        public async Task<IEnumerable<TEntity>> BulkCreateItemsAsync<TEntity>(IList<TEntity> entities) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> BulkReplaceItemsAsync<TEntity>(IList<TEntity> entities) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> BulkUpsertItemsAsync<TEntity>(IList<TEntity> entities) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
