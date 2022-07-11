using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.Models.v4
{
    /// <summary>
    /// The privacy of a repository
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepoAddMemberAuditEntryVisibility
    {
        /// <summary>
        /// The repository is visible only to users in the same business.
        /// </summary>
        [EnumMember(Value = "INTERNAL")]
        Internal,

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
    }
}