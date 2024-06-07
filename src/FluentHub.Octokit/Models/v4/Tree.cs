// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a Git tree.
	/// </summary>
	public class Tree
	{
		/// <summary>
		/// An abbreviated version of the Git object ID
		/// </summary>
		public string AbbreviatedOid { get; set; }

		/// <summary>
		/// The HTTP path for this Git object
		/// </summary>
		public string CommitResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for this Git object
		/// </summary>
		public string CommitUrl { get; set; }

		/// <summary>
		/// A list of tree entries.
		/// </summary>
		public List<TreeEntry> Entries { get; set; }

		/// <summary>
		/// The Node ID of the Tree object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The Git object ID
		/// </summary>
		public string Oid { get; set; }

		/// <summary>
		/// The Repository the Git object belongs to
		/// </summary>
		public Repository Repository { get; set; }
	}
}
