using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CosmosToolbox.Core.Enums;

namespace CosmosToolbox.Core.Options
{
    /// <summary>
    /// options used to configure a Cosmos Database
    /// </summary>
    public sealed class DatabaseOptions
    {
        /// <summary>
        /// Database Id
        /// </summary>
        /// <value></value>
        public string Id { get; set; }

        /// <summary>
        /// specify a value to set shared throughput at database level
        /// </summary>
        /// <value></value>
        public int? Throughput { get; set; }

        /// <summary>
        /// database api associated with this database (defaults to SQL)
        /// </summary>
        public DatabaseApi Api { get; set; } = DatabaseApi.SqlApi;

        /// <summary>
        /// any containers to be created within this database
        /// </summary>
        /// <value></value>
        public IEnumerable<ContainerOptions> Containers { get; set; }

        /// <summary>
        /// validates configuration is correct
        /// </summary>
        public void Validate()
        {
            var sbErrors = new StringBuilder();

            if (string.IsNullOrEmpty(Id))
                sbErrors.Append($"{nameof(Id)} was null or empty");

            if (Api == null)
                sbErrors.Append($"{nameof(Api)} was null or empty");

            if (!(Containers?.Any() ?? false))
                sbErrors.Append($"no {nameof(Containers)} were specified");

            var errors = sbErrors.ToString();
            if (!string.IsNullOrEmpty(errors))
                throw new ArgumentException($"{nameof(DatabaseOptions)} Errors: {errors}");
        }
    }
}