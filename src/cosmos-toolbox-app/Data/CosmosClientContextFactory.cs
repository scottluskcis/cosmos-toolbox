using System;
using System.Collections.Concurrent;
using System.Linq;
using CosmosToolbox.App.Extensions;
using CosmosToolbox.Core.Data;
using CosmosToolbox.Core.Enums;
using CosmosToolbox.Core.Options;
using Microsoft.Extensions.Logging;

namespace CosmosToolbox.App.Data
{
    public sealed class CosmosClientContextFactory : IClientContextFactory, IDisposable
    {
        private ConcurrentDictionary<string, IClientContext> _clientContexts;
        private readonly ILoggerFactory _loggerFactory;

        public CosmosClientContextFactory(ILoggerFactory loggerFactory)
        {
            _clientContexts = new ConcurrentDictionary<string, IClientContext>();
            _loggerFactory = loggerFactory;
        }

        public IClientContext Create(ClientContextOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            var key = options.GetContextIdentifier();
            var context = _clientContexts.GetOrAdd(key, CreateContext(options));
            return context;
        }

        private IClientContext CreateContext(ClientContextOptions options)
        {
            options.Validate();

            if (options.Database.Api.Equals(DatabaseApi.SqlApi))
                return new CosmosSqlApiClientContext(options, _loggerFactory.CreateLogger<CosmosSqlApiClientContext>());
            else if(options.Database.Api.Equals(DatabaseApi.TableApi))
                return new CosmosTableApiClientContext(options, _loggerFactory.CreateLogger<CosmosTableApiClientContext>());
            else 
                throw new NotSupportedException($"api '{options.Database.Api}' is not currently supported in {nameof(CosmosClientContextFactory)}");
        }

        public void Dispose()
        {
            if (!(_clientContexts?.Any() ?? false))
                return;

            foreach (var clientContext in _clientContexts)
                if(clientContext.Value is IDisposable disposableValue)
                    disposableValue.Dispose();

            _clientContexts = null;
        }
    }
}
