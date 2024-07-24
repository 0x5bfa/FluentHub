// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents an actor in a Git commit (ie. an author or committer).
	/// </summary>
	public class GitActor
	{
		/// <summary>
		/// A URL pointing to the author's public avatar.
		/// </summary>
		/// <param name="size">The size of the resulting square image.</param>
		public string AvatarUrl { get; set; }

		/// <summary>
		/// The timestamp of the Git action (authoring or committing).
		/// </summary>
		public string Date { get; set; }

		/// <summary>
		/// The email in the Git commit.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// The name in the Git commit.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The GitHub user corresponding to the email field. Null if no such user exists.
		/// </summary>
		public User User { get; set; }
	}
}
