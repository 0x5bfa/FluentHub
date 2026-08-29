using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The repository's visibility level.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepositoryVisibility
    {
        /// <summary>
        /// The repository is visible only to those with explicit access.
        /// </summary>
        [EnumMember(Value = "PRIVATE")]
        Private,

        /// <summary>
        /// The repository is visible to everyone.
        /// </summary>
        [EnumMember(Value = "PUBLIC")]
        Public,

        /// <summary>
        /// The repository is visible only to users in the same business.
        /// </summary>
        [EnumMember(Value = "INTERNAL")]
        Internal,
    }
}