using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible subject types of a pull request review comment.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PullRequestReviewThreadSubjectType
    {
        /// <summary>
        /// A comment that has been made against the line of a pull request
        /// </summary>
        [EnumMember(Value = "LINE")]
        Line,

        /// <summary>
        /// A comment that has been made against the file of a pull request
        /// </summary>
        [EnumMember(Value = "FILE")]
        File,
    }
}