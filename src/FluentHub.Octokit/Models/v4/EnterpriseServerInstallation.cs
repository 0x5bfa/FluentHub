// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An Enterprise Server installation.
	/// </summary>
	public class EnterpriseServerInstallation
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
		/// The customer name to which the Enterprise Server installation belongs.
		/// </summary>
		public string CustomerName { get; set; }

		/// <summary>
		/// The host name of the Enterprise Server installation.
		/// </summary>
		public string HostName { get; set; }

		/// <summary>
		/// The Node ID of the EnterpriseServerInstallation object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Whether or not the installation is connected to an Enterprise Server installation via GitHub Connect.
		/// </summary>
		public bool IsConnected { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }

		/// <summary>
		/// User accounts on this Enterprise Server installation.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for Enterprise Server user accounts returned from the connection.</param>
		public EnterpriseServerUserAccountConnection UserAccounts { get; set; }

		/// <summary>
		/// User accounts uploads for the Enterprise Server installation.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for Enterprise Server user accounts uploads returned from the connection.</param>
		public EnterpriseServerUserAccountsUploadConnection UserAccountsUploads { get; set; }
	}
}
