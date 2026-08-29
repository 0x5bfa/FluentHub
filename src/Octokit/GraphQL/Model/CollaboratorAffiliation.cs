using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Collaborators affiliation level with a subject.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CollaboratorAffiliation
    {
        /// <summary>
        /// All outside collaborators of an organization-owned subject.
        /// </summary>
        [EnumMember(Value = "OUTSIDE")]
        Outside,

        /// <summary>
        /// All collaborators with permissions to an organization-owned subject, regardless of organization membership status.
        /// </summary>
        [EnumMember(Value = "DIRECT")]
        Direct,

        /// <summary>
        /// All collaborators the authenticated user can see.
        /// </summary>
        [EnumMember(Value = "ALL")]
        All,
    }
}