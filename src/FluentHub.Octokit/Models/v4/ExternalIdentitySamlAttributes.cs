// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// SAML attributes for the External Identity
	/// </summary>
	public class ExternalIdentitySamlAttributes
	{
		/// <summary>
		/// SAML Identity attributes
		/// </summary>
		public List<ExternalIdentityAttribute> Attributes { get; set; }

		/// <summary>
		/// The emails associated with the SAML identity
		/// </summary>
		public List<UserEmailMetadata> Emails { get; set; }

		/// <summary>
		/// Family name of the SAML identity
		/// </summary>
		public string FamilyName { get; set; }

		/// <summary>
		/// Given name of the SAML identity
		/// </summary>
		public string GivenName { get; set; }

		/// <summary>
		/// The groups linked to this identity in IDP
		/// </summary>
		public List<string> Groups { get; set; }

		/// <summary>
		/// The NameID of the SAML identity
		/// </summary>
		public string NameId { get; set; }

		/// <summary>
		/// The userName of the SAML identity
		/// </summary>
		public string Username { get; set; }
	}
}
