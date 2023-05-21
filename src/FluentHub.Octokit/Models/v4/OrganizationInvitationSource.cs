using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.Models.v4
{
    /// <summary>
    /// The possible organization invitation sources.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrganizationInvitationSource
    {
        /// <summary>
        /// The invitation was sent before this feature was added
        /// </summary>
        [EnumMember(Value = "UNKNOWN")]
        Unknown,

        /// <summary>
        /// The invitation was created from the web interface or from API
        /// </summary>
        [EnumMember(Value = "MEMBER")]
        Member,

        /// <summary>
        /// The invitation was created from SCIM
        /// </summary>
        [EnumMember(Value = "SCIM")]
        Scim,
    }
}