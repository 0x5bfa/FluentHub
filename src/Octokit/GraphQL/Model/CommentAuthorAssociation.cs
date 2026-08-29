using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// A comment author association with repository.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CommentAuthorAssociation
    {
        /// <summary>
        /// Author is a member of the organization that owns the repository.
        /// </summary>
        [EnumMember(Value = "MEMBER")]
        Member,

        /// <summary>
        /// Author is the owner of the repository.
        /// </summary>
        [EnumMember(Value = "OWNER")]
        Owner,

        /// <summary>
        /// Author is a placeholder for an unclaimed user.
        /// </summary>
        [EnumMember(Value = "MANNEQUIN")]
        Mannequin,

        /// <summary>
        /// Author has been invited to collaborate on the repository.
        /// </summary>
        [EnumMember(Value = "COLLABORATOR")]
        Collaborator,

        /// <summary>
        /// Author has previously committed to the repository.
        /// </summary>
        [EnumMember(Value = "CONTRIBUTOR")]
        Contributor,

        /// <summary>
        /// Author has not previously committed to the repository.
        /// </summary>
        [EnumMember(Value = "FIRST_TIME_CONTRIBUTOR")]
        FirstTimeContributor,

        /// <summary>
        /// Author has not previously committed to GitHub.
        /// </summary>
        [EnumMember(Value = "FIRST_TIMER")]
        FirstTimer,

        /// <summary>
        /// Author has no association with the repository.
        /// </summary>
        [EnumMember(Value = "NONE")]
        None,
    }
}