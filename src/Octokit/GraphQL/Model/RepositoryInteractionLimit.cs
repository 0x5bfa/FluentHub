using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// A repository interaction limit.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepositoryInteractionLimit
    {
        /// <summary>
        /// Users that have recently created their account will be unable to interact with the repository.
        /// </summary>
        [EnumMember(Value = "EXISTING_USERS")]
        ExistingUsers,

        /// <summary>
        /// Users that have not previously committed to a repository’s default branch will be unable to interact with the repository.
        /// </summary>
        [EnumMember(Value = "CONTRIBUTORS_ONLY")]
        ContributorsOnly,

        /// <summary>
        /// Users that are not collaborators will not be able to interact with the repository.
        /// </summary>
        [EnumMember(Value = "COLLABORATORS_ONLY")]
        CollaboratorsOnly,

        /// <summary>
        /// No interaction limits are enabled.
        /// </summary>
        [EnumMember(Value = "NO_LIMIT")]
        NoLimit,
    }
}