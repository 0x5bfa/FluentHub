using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which Enterprise Server user account email connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnterpriseServerUserAccountEmailOrderField
    {
        /// <summary>
        /// Order emails by email
        /// </summary>
        [EnumMember(Value = "EMAIL")]
        Email,
    }
}