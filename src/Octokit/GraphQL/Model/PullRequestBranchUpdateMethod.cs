using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible methods for updating a pull request's head branch with the base branch.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PullRequestBranchUpdateMethod
    {
        /// <summary>
        /// Update branch via merge
        /// </summary>
        [EnumMember(Value = "MERGE")]
        Merge,

        /// <summary>
        /// Update branch via rebase
        /// </summary>
        [EnumMember(Value = "REBASE")]
        Rebase,
    }
}