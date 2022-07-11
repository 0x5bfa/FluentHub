using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.Models.v4
{
    /// <summary>
    /// Properties by which Audit Log connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AuditLogOrderField
    {
        /// <summary>
        /// Order audit log entries by timestamp
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,
    }
}