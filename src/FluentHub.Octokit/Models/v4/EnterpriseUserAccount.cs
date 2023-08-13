// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An account for a user who is an admin of an enterprise or a member of an enterprise through one or more organizations.
	/// </summary>
	public class EnterpriseUserAccount
	{
		/// <summary>
		/// A URL pointing to the enterprise user account's public avatar.
		/// </summary>
		/// <param name="size">The size of the resulting square image.</param>
		public string AvatarUrl { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The enterprise in which this user account exists.
		/// </summary>
		public Enterprise Enterprise { get; set; }

		/// <summary>
		/// A list of Enterprise Server installations this user is a member of.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for installations returned from the connection.</param>
		/// <param name="query">The search string to look for.</param>
		/// <param name="role">The role of the user in the installation.</param>
		public EnterpriseServerInstallationMembershipConnection EnterpriseInstallations { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// An identifier for the enterprise user account, a login or email address
		/// </summary>
		public string Login { get; set; }

		/// <summary>
		/// The name of the enterprise user account
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// A list of enterprise organizations this user is a member of.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for organizations returned from the connection.</param>
		/// <param name="query">The search string to look for.</param>
		/// <param name="role">The role of the user in the enterprise organization.</param>
		public EnterpriseOrganizationMembershipConnection Organizations { get; set; }

		/// <summary>
		/// The HTTP path for this user.
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }

		/// <summary>
		/// The HTTP URL for this user.
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// The user within the enterprise.
		/// </summary>
		public User User { get; set; }
	}
}
