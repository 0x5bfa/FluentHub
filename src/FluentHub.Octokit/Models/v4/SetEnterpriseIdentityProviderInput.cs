// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated input type of SetEnterpriseIdentityProvider
	/// </summary>
	public class SetEnterpriseIdentityProviderInput
	{
		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }

		/// <summary>
		/// The ID of the enterprise on which to set an identity provider.
		/// </summary>
		public ID EnterpriseId { get; set; }

		/// <summary>
		/// The URL endpoint for the identity provider's SAML SSO.
		/// </summary>
		public string SsoUrl { get; set; }

		/// <summary>
		/// The Issuer Entity ID for the SAML identity provider
		/// </summary>
		public string Issuer { get; set; }

		/// <summary>
		/// The x509 certificate used by the identity provider to sign assertions and responses.
		/// </summary>
		public string IdpCertificate { get; set; }

		/// <summary>
		/// The signature algorithm used to sign SAML requests for the identity provider.
		/// </summary>
		public SamlSignatureAlgorithm SignatureMethod { get; set; }

		/// <summary>
		/// The digest algorithm used to sign SAML requests for the identity provider.
		/// </summary>
		public SamlDigestAlgorithm DigestMethod { get; set; }
	}
}
