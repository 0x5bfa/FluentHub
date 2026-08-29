using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The privacy of a sponsorship
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SponsorshipPrivacy
    {
        /// <summary>
        /// Public
        /// </summary>
        [EnumMember(Value = "PUBLIC")]
        Public,

        /// <summary>
        /// Private
        /// </summary>
        [EnumMember(Value = "PRIVATE")]
        Private,
    }
}