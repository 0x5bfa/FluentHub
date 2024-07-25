// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A project column and a list of its issues and PRs.
	/// </summary>
	public class ProjectColumnImport
	{
		/// <summary>
		/// The name of the column.
		/// </summary>
		public string ColumnName { get; set; }

		/// <summary>
		/// The position of the column, starting from 0.
		/// </summary>
		public int Position { get; set; }

		/// <summary>
		/// A list of issues and pull requests in the column.
		/// </summary>
		public List<ProjectCardImport> Issues { get; set; }
	}
}
