using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which sponsorable connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SponsorableOrderField
    {
        /// <summary>
        /// Order sponsorable entities by login (username).
        /// </summary>
        [EnumMember(Value = "LOGIN")]
        Login,
    }
}