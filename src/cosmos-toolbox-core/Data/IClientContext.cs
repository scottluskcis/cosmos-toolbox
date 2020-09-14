using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CosmosToolbox.Core.Data
{
    public interface IClientContext
    {
        Task<TEntity> ReadItemAsync<TEntity>(
            string id,
            string partitionKey,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity;

        Task<TEntity> CreateItemAsync<TEntity>(
            TEntity item,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity;

        Task<TEntity> ReplaceItemAsync<TEntity>(
            TEntity item,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity;

        Task<TEntity> UpsertItemAsync<TEntity>(
            TEntity item,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity;

        Task<TEntity> DeleteItemAsync<TEntity>(
            string id,
            string partitionKey,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity;
    }
    
    public interface IQueryClientContext
    {
        Task<IEnumerable<TEntity>> ReadItemsAsync<TEntity>(
            Expression<Func<TEntity, bool>> predicate = null,
            string partitionKey = "",
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity;

        Task<IEnumerable<TEntity>> QueryItemsAsync<TEntity>(
            string sql,
            string partitionKey = "",
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity;
    }

    public interface IBulkExecutorClientContext
    {
        Task<IEnumerable<TEntity>> BulkCreateItemsAsync<TEntity>(
            IList<TEntity> entities)
            where TEntity : class, IEntity;

        Task<IEnumerable<TEntity>> BulkReplaceItemsAsync<TEntity>(
            IList<TEntity> entities)
            where TEntity : class, IEntity;

        Task<IEnumerable<TEntity>> BulkUpsertItemsAsync<TEntity>(
            IList<TEntity> entities)
            where TEntity : class, IEntity;
    }
}