// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
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
