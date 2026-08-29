using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which discussion poll option connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DiscussionPollOptionOrderField
    {
        /// <summary>
        /// Order poll options by the order that the poll author specified when creating the poll.
        /// </summary>
        [EnumMember(Value = "AUTHORED_ORDER")]
        AuthoredOrder,

        /// <summary>
        /// Order poll options by the number of votes it has.
        /// </summary>
        [EnumMember(Value = "VOTE_COUNT")]
        VoteCount,
    }
}