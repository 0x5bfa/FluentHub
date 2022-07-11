using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.v4.Model
{
    /// <summary>
    /// The state of an OAuth Application when it was created.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OauthApplicationCreateAuditEntryState
    {
        /// <summary>
        /// The OAuth Application was active and allowed to have OAuth Accesses.
        /// </summary>
        [EnumMember(Value = "ACTIVE")]
        Active,

        /// <summary>
        /// The OAuth Application was suspended from generating OAuth Accesses due to abuse or security concerns.
        /// </summary>
        [EnumMember(Value = "SUSPENDED")]
        Suspended,

        /// <summary>
        /// The OAuth Application was in the process of being deleted.
        /// </summary>
        [EnumMember(Value = "PENDING_DELETION")]
        PendingDeletion,
    }
}