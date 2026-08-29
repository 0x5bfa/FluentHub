using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which Enterprise Server user account connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnterpriseServerUserAccountOrderField
    {
        /// <summary>
        /// Order user accounts by login
        /// </summary>
        [EnumMember(Value = "LOGIN")]
        Login,

        /// <summary>
        /// Order user accounts by creation time on the Enterprise Server installation
        /// </summary>
        [EnumMember(Value = "REMOTE_CREATED_AT")]
        RemoteCreatedAt,
    }
}