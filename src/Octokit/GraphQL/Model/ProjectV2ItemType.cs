using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The type of a project item.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProjectV2ItemType
    {
        /// <summary>
        /// Issue
        /// </summary>
        [EnumMember(Value = "ISSUE")]
        Issue,

        /// <summary>
        /// Pull Request
        /// </summary>
        [EnumMember(Value = "PULL_REQUEST")]
        PullRequest,

        /// <summary>
        /// Draft Issue
        /// </summary>
        [EnumMember(Value = "DRAFT_ISSUE")]
        DraftIssue,

        /// <summary>
        /// Redacted Item
        /// </summary>
        [EnumMember(Value = "REDACTED")]
        Redacted,
    }
}