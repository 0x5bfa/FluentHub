// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An identity provider configured to provision identities for an enterprise. Visible to enterprise owners or enterprise owners' personal access tokens (classic) with read:enterprise or admin:enterprise scope.
	/// </summary>
	public class EnterpriseIdentityProvider
	{
		/// <summary>
		/// The digest algorithm used to sign SAML requests for the identity provider.
		/// </summary>
		public SamlDigestAlgorithm? DigestMethod { get; set; }

		/// <summary>
		/// The enterprise this identity provider belongs to.
		/// </summary>
		public Enterprise Enterprise { get; set; }

		/// <summary>
		/// ExternalIdentities provisioned by this identity provider.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="login">Filter to external identities with the users login</param>
		/// <param name="membersOnly">Filter to external identities with valid org membership only</param>
		/// <param name="userName">Filter to external identities with the users userName/NameID attribute</param>
		public ExternalIdentityConnection ExternalIdentities { get; set; }

		/// <summary>
		/// The Node ID of the EnterpriseIdentityProvider object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The x509 certificate used by the identity provider to sign assertions and responses.
		/// </summary>
		public string IdpCertificate { get; set; }

		/// <summary>
		/// The Issuer Entity ID for the SAML identity provider.
		/// </summary>
		public string Issuer { get; set; }

		/// <summary>
		/// Recovery codes that can be used by admins to access the enterprise if the identity provider is unavailable.
		/// </summary>
		public List<string> RecoveryCodes { get; set; }

		/// <summary>
		/// The signature algorithm used to sign SAML requests for the identity provider.
		/// </summary>
		public SamlSignatureAlgorithm? SignatureMethod { get; set; }

		/// <summary>
		/// The URL endpoint for the identity provider's SAML SSO.
		/// </summary>
		public string SsoUrl { get; set; }
	}
}
