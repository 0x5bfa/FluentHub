// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A user accounts upload from an Enterprise Server installation.
	/// </summary>
	public class EnterpriseServerUserAccountsUpload
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
		/// The enterprise to which this upload belongs.
		/// </summary>
		public Enterprise Enterprise { get; set; }

		/// <summary>
		/// The Enterprise Server installation for which this upload was generated.
		/// </summary>
		public EnterpriseServerInstallation EnterpriseServerInstallation { get; set; }

		/// <summary>
		/// The Node ID of the EnterpriseServerUserAccountsUpload object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The name of the file uploaded.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The synchronization state of the upload
		/// </summary>
		public EnterpriseServerUserAccountsUploadSyncState SyncState { get; set; }

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
