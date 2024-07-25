// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Metadata for a repository membership for org.restore_member actions
	/// </summary>
	public class OrgRestoreMemberMembershipRepositoryAuditEntryData
	{
		/// <summary>
		/// The repository associated with the action
		/// </summary>
		public Repository Repository { get; set; }

		/// <summary>
		/// The name of the repository
		/// </summary>
		public string RepositoryName { get; set; }

		/// <summary>
		/// The HTTP path for the repository
		/// </summary>
		public string RepositoryResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for the repository
		/// </summary>
		public string RepositoryUrl { get; set; }
	}
}
