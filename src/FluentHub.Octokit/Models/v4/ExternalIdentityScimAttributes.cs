// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// SCIM attributes for the External Identity
	/// </summary>
	public class ExternalIdentityScimAttributes
	{
		/// <summary>
		/// The emails associated with the SCIM identity
		/// </summary>
		public List<UserEmailMetadata> Emails { get; set; }

		/// <summary>
		/// Family name of the SCIM identity
		/// </summary>
		public string FamilyName { get; set; }

		/// <summary>
		/// Given name of the SCIM identity
		/// </summary>
		public string GivenName { get; set; }

		/// <summary>
		/// The groups linked to this identity in IDP
		/// </summary>
		public List<string> Groups { get; set; }

		/// <summary>
		/// The userName of the SCIM identity
		/// </summary>
		public string Username { get; set; }
	}
}
