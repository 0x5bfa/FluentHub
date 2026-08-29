using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Indicates where an interaction limit is configured.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepositoryInteractionLimitOrigin
    {
        /// <summary>
        /// A limit that is configured at the repository level.
        /// </summary>
        [EnumMember(Value = "REPOSITORY")]
        Repository,

        /// <summary>
        /// A limit that is configured at the organization level.
        /// </summary>
        [EnumMember(Value = "ORGANIZATION")]
        Organization,

        /// <summary>
        /// A limit that is configured at the user-wide level.
        /// </summary>
        [EnumMember(Value = "USER")]
        User,
    }
}