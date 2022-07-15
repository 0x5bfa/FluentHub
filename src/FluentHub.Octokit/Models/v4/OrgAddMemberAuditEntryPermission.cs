using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.Models.v4
{
    /// <summary>
    /// The permissions available to members on an Organization.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrgAddMemberAuditEntryPermission
    {
        /// <summary>
        /// Can read and clone repositories.
        /// </summary>
        [EnumMember(Value = "READ")]
        Read,

        /// <summary>
        /// Can read, clone, push, and add collaborators to repositories.
        /// </summary>
        [EnumMember(Value = "ADMIN")]
        Admin,
    }
}