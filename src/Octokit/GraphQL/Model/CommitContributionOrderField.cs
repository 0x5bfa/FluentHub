using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which commit contribution connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CommitContributionOrderField
    {
        /// <summary>
        /// Order commit contributions by when they were made.
        /// </summary>
        [EnumMember(Value = "OCCURRED_AT")]
        OccurredAt,

        /// <summary>
        /// Order commit contributions by how many commits they represent.
        /// </summary>
        [EnumMember(Value = "COMMIT_COUNT")]
        CommitCount,
    }
}