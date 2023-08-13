// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An external identity provisioned by SAML SSO or SCIM. If SAML is configured on the organization, the external identity is visible to (1) organization owners, (2) organization owners' personal access tokens (classic) with read:org or admin:org scope, (3) GitHub App with an installation token with read or write access to members. If SAML is configured on the enterprise, the external identity is visible to (1) enterprise owners, (2) enterprise owners' personal access tokens (classic) with read:enterprise or admin:enterprise scope.
	/// </summary>
	public class ExternalIdentity
	{
		/// <summary>
		/// The GUID for this identity
		/// </summary>
		public string Guid { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// Organization invitation for this SCIM-provisioned external identity
		/// </summary>
		public OrganizationInvitation OrganizationInvitation { get; set; }

		/// <summary>
		/// SAML Identity attributes
		/// </summary>
		public ExternalIdentitySamlAttributes SamlIdentity { get; set; }

		/// <summary>
		/// SCIM Identity attributes
		/// </summary>
		public ExternalIdentityScimAttributes ScimIdentity { get; set; }

		/// <summary>
		/// User linked to this external identity. Will be NULL if this identity has not been claimed by an organization member.
		/// </summary>
		public User User { get; set; }
	}
}
