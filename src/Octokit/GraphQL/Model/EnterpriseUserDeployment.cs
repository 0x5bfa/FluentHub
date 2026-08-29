using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible GitHub Enterprise deployments where this user can exist.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnterpriseUserDeployment
    {
        /// <summary>
        /// The user is part of a GitHub Enterprise Cloud deployment.
        /// </summary>
        [EnumMember(Value = "CLOUD")]
        Cloud,

        /// <summary>
        /// The user is part of a GitHub Enterprise Server deployment.
        /// </summary>
        [EnumMember(Value = "SERVER")]
        Server,
    }
}