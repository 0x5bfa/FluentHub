using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which repository migrations can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepositoryMigrationOrderField
    {
        /// <summary>
        /// Order mannequins why when they were created.
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,
    }
}