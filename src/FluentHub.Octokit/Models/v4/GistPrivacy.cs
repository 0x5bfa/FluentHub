using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.Models.v4
{
    /// <summary>
    /// The privacy of a Gist
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GistPrivacy
    {
        /// <summary>
        /// Public
        /// </summary>
        [EnumMember(Value = "PUBLIC")]
        Public,

        /// <summary>
        /// Secret
        /// </summary>
        [EnumMember(Value = "SECRET")]
        Secret,

        /// <summary>
        /// Gists that are public and secret
        /// </summary>
        [EnumMember(Value = "ALL")]
        All,
    }
}