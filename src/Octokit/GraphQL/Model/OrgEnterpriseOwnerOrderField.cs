using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which enterprise owners can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrgEnterpriseOwnerOrderField
    {
        /// <summary>
        /// Order enterprise owners by login.
        /// </summary>
        [EnumMember(Value = "LOGIN")]
        Login,
    }
}