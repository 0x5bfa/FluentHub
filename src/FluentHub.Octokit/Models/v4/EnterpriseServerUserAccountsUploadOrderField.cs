using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.Models.v4
{
    /// <summary>
    /// Properties by which Enterprise Server user accounts upload connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnterpriseServerUserAccountsUploadOrderField
    {
        /// <summary>
        /// Order user accounts uploads by creation time
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,
    }
}