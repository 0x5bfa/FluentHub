using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible team privacy values.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TeamPrivacy
    {
        /// <summary>
        /// A secret team can only be seen by its members.
        /// </summary>
        [EnumMember(Value = "SECRET")]
        Secret,

        /// <summary>
        /// A visible team can be seen and @mentioned by every member of the organization.
        /// </summary>
        [EnumMember(Value = "VISIBLE")]
        Visible,
    }
}