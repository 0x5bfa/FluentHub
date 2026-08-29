using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which Enterprise Server installation connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnterpriseServerInstallationOrderField
    {
        /// <summary>
        /// Order Enterprise Server installations by host name
        /// </summary>
        [EnumMember(Value = "HOST_NAME")]
        HostName,

        /// <summary>
        /// Order Enterprise Server installations by customer name
        /// </summary>
        [EnumMember(Value = "CUSTOMER_NAME")]
        CustomerName,

        /// <summary>
        /// Order Enterprise Server installations by creation time
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,
    }
}