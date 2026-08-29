using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible viewed states of a file .
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FileViewedState
    {
        /// <summary>
        /// The file has new changes since last viewed.
        /// </summary>
        [EnumMember(Value = "DISMISSED")]
        Dismissed,

        /// <summary>
        /// The file has been marked as viewed.
        /// </summary>
        [EnumMember(Value = "VIEWED")]
        Viewed,

        /// <summary>
        /// The file has not been marked as viewed.
        /// </summary>
        [EnumMember(Value = "UNVIEWED")]
        Unviewed,
    }
}