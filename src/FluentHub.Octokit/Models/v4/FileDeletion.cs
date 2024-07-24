// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A command to delete the file at the given path as part of a commit.
	/// </summary>
	public class FileDeletion
	{
		/// <summary>
		/// The path to delete
		/// </summary>
		public string Path { get; set; }
	}
}
