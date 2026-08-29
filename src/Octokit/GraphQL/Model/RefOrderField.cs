using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which ref connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RefOrderField
    {
        /// <summary>
        /// Order refs by underlying commit date if the ref prefix is refs/tags/
        /// </summary>
        [EnumMember(Value = "TAG_COMMIT_DATE")]
        TagCommitDate,

        /// <summary>
        /// Order refs by their alphanumeric name
        /// </summary>
        [EnumMember(Value = "ALPHABETICAL")]
        Alphabetical,
    }
}