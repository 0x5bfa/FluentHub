using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The actor's type.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ActorType
    {
        /// <summary>
        /// Indicates a user actor.
        /// </summary>
        [EnumMember(Value = "USER")]
        User,

        /// <summary>
        /// Indicates a team actor.
        /// </summary>
        [EnumMember(Value = "TEAM")]
        Team,
    }
}