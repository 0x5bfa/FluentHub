using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The reason an outside collaborator was removed from an Organization.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrgRemoveOutsideCollaboratorAuditEntryReason
    {
        /// <summary>
        /// The organization required 2FA of its billing managers and this user did not have 2FA enabled.
        /// </summary>
        [EnumMember(Value = "TWO_FACTOR_REQUIREMENT_NON_COMPLIANCE")]
        TwoFactorRequirementNonCompliance,

        /// <summary>
        /// SAML external identity missing
        /// </summary>
        [EnumMember(Value = "SAML_EXTERNAL_IDENTITY_MISSING")]
        SamlExternalIdentityMissing,
    }
}