// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A user account on an Enterprise Server installation.
	/// </summary>
	public class EnterpriseServerUserAccount
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
		/// User emails belonging to this user account.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for Enterprise Server user account emails returned from the connection.</param>
		public EnterpriseServerUserAccountEmailConnection Emails { get; set; }

		/// <summary>
		/// The Enterprise Server installation on which this user account exists.
		/// </summary>
		public EnterpriseServerInstallation EnterpriseServerInstallation { get; set; }

		/// <summary>
		/// The Node ID of the EnterpriseServerUserAccount object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Whether the user account is a site administrator on the Enterprise Server installation.
		/// </summary>
		public bool IsSiteAdmin { get; set; }

		/// <summary>
		/// The login of the user account on the Enterprise Server installation.
		/// </summary>
		public string Login { get; set; }

		/// <summary>
		/// The profile name of the user account on the Enterprise Server installation.
		/// </summary>
		public string ProfileName { get; set; }

		/// <summary>
		/// The date and time when the user account was created on the Enterprise Server installation.
		/// </summary>
		public DateTimeOffset RemoteCreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "The date and time when the user account was created on the Enterprise Server installation."
		/// <summary>
		public string RemoteCreatedAtHumanized { get; set; }

		/// <summary>
		/// The ID of the user account on the Enterprise Server installation.
		/// </summary>
		public int RemoteUserId { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }
	}
}
