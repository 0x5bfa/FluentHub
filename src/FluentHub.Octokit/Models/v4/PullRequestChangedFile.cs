// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A file changed in a pull request.
	/// </summary>
	public class PullRequestChangedFile
	{
		/// <summary>
		/// The number of additions to the file.
		/// </summary>
		public int Additions { get; set; }

		/// <summary>
		/// How the file was changed in this PullRequest
		/// </summary>
		public PatchStatus ChangeType { get; set; }

		/// <summary>
		/// The number of deletions to the file.
		/// </summary>
		public int Deletions { get; set; }

		/// <summary>
		/// The path of the file.
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// The state of the file for the viewer.
		/// </summary>
		public FileViewedState ViewerViewedState { get; set; }
	}
}
