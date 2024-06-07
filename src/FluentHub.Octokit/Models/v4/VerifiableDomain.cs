// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A domain that can be verified or approved for an organization or an enterprise.
	/// </summary>
	public class VerifiableDomain
	{
		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		/// <summary>
		/// The DNS host name that should be used for verification.
		/// </summary>
		public string DnsHostName { get; set; }

		/// <summary>
		/// The unicode encoded domain.
		/// </summary>
		public string Domain { get; set; }

		/// <summary>
		/// Whether a TXT record for verification with the expected host name was found.
		/// </summary>
		public bool HasFoundHostName { get; set; }

		/// <summary>
		/// Whether a TXT record for verification with the expected verification token was found.
		/// </summary>
		public bool HasFoundVerificationToken { get; set; }

		/// <summary>
		/// The Node ID of the VerifiableDomain object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Whether or not the domain is approved.
		/// </summary>
		public bool IsApproved { get; set; }

		/// <summary>
		/// Whether this domain is required to exist for an organization or enterprise policy to be enforced.
		/// </summary>
		public bool IsRequiredForPolicyEnforcement { get; set; }

		/// <summary>
		/// Whether or not the domain is verified.
		/// </summary>
		public bool IsVerified { get; set; }

		/// <summary>
		/// The owner of the domain.
		/// </summary>
		public VerifiableDomainOwner Owner { get; set; }

		/// <summary>
		/// The punycode encoded domain.
		/// </summary>
		public string PunycodeEncodedDomain { get; set; }

		/// <summary>
		/// The time that the current verification token will expire.
		/// </summary>
		public DateTimeOffset? TokenExpirationTime { get; set; }

		/// <summary>
		/// Humanized string of "The time that the current verification token will expire."
		/// <summary>
		public string TokenExpirationTimeHumanized { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }

		/// <summary>
		/// The current verification token for the domain.
		/// </summary>
		public string VerificationToken { get; set; }
	}
}
