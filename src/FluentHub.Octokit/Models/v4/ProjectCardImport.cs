// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An issue or PR and its owning repository to be used in a project card.
	/// </summary>
	public class ProjectCardImport
	{
		/// <summary>
		/// Repository name with owner (owner/repository).
		/// </summary>
		public string Repository { get; set; }

		/// <summary>
		/// The issue or pull request number.
		/// </summary>
		public int Number { get; set; }
	}
}
