// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An email belonging to a user account on an Enterprise Server installation.
	/// </summary>
	public class EnterpriseServerUserAccountEmail
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
		/// The email address.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// The Node ID of the EnterpriseServerUserAccountEmail object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Indicates whether this is the primary email of the associated user account.
		/// </summary>
		public bool IsPrimary { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }

		/// <summary>
		/// The user account to which the email belongs.
		/// </summary>
		public EnterpriseServerUserAccount UserAccount { get; set; }
	}
}
