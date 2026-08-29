using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which sponsor connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SponsorOrderField
    {
        /// <summary>
        /// Order sponsorable entities by login (username).
        /// </summary>
        [EnumMember(Value = "LOGIN")]
        Login,

        /// <summary>
        /// Order sponsors by their relevance to the viewer.
        /// </summary>
        [EnumMember(Value = "RELEVANCE")]
        Relevance,
    }
}