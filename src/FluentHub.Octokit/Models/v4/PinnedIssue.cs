// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A Pinned Issue is a issue pinned to a repository's index page.
	/// </summary>
	public class PinnedIssue
	{
		/// <summary>
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		/// <summary>
		/// Identifies the primary key from the database as a BigInt.
		/// </summary>
		public string FullDatabaseId { get; set; }

		/// <summary>
		/// The Node ID of the PinnedIssue object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The issue that was pinned.
		/// </summary>
		public Issue Issue { get; set; }

		/// <summary>
		/// The actor that pinned this issue.
		/// </summary>
		public IActor PinnedBy { get; set; }

		/// <summary>
		/// The repository that this issue was pinned to.
		/// </summary>
		public Repository Repository { get; set; }
	}
}
