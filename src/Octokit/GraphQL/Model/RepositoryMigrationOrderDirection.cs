using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Possible directions in which to order a list of repository migrations when provided an `orderBy` argument.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepositoryMigrationOrderDirection
    {
        /// <summary>
        /// Specifies an ascending order for a given `orderBy` argument.
        /// </summary>
        [EnumMember(Value = "ASC")]
        Asc,

        /// <summary>
        /// Specifies a descending order for a given `orderBy` argument.
        /// </summary>
        [EnumMember(Value = "DESC")]
        Desc,
    }
}