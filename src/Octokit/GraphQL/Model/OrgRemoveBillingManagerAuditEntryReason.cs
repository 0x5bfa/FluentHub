using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The reason a billing manager was removed from an Organization.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrgRemoveBillingManagerAuditEntryReason
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

        /// <summary>
        /// SAML SSO enforcement requires an external identity
        /// </summary>
        [EnumMember(Value = "SAML_SSO_ENFORCEMENT_REQUIRES_EXTERNAL_IDENTITY")]
        SamlSsoEnforcementRequiresExternalIdentity,
    }
}