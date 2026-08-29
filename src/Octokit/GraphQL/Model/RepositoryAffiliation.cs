using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The affiliation of a user to a repository
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepositoryAffiliation
    {
        /// <summary>
        /// Repositories that are owned by the authenticated user.
        /// </summary>
        [EnumMember(Value = "OWNER")]
        Owner,

        /// <summary>
        /// Repositories that the user has been added to as a collaborator.
        /// </summary>
        [EnumMember(Value = "COLLABORATOR")]
        Collaborator,

        /// <summary>
        /// Repositories that the user has access to through being a member of an organization. This includes every repository on every team that the user is on.
        /// </summary>
        [EnumMember(Value = "ORGANIZATION_MEMBER")]
        OrganizationMember,
    }
}