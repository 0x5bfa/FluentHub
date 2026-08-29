using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible organization invitation roles.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrganizationInvitationRole
    {
        /// <summary>
        /// The user is invited to be a direct member of the organization.
        /// </summary>
        [EnumMember(Value = "DIRECT_MEMBER")]
        DirectMember,

        /// <summary>
        /// The user is invited to be an admin of the organization.
        /// </summary>
        [EnumMember(Value = "ADMIN")]
        Admin,

        /// <summary>
        /// The user is invited to be a billing manager of the organization.
        /// </summary>
        [EnumMember(Value = "BILLING_MANAGER")]
        BillingManager,

        /// <summary>
        /// The user's previous role will be reinstated.
        /// </summary>
        [EnumMember(Value = "REINSTATE")]
        Reinstate,
    }
}