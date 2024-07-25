// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Specifies an author for filtering Git commits.
	/// </summary>
	public class CommitAuthor
	{
		/// <summary>
		/// ID of a User to filter by. If non-null, only commits authored by this user will be returned. This field takes precedence over emails.
		/// </summary>
		public ID? Id { get; set; }

		/// <summary>
		/// Email addresses to filter by. Commits authored by any of the specified email addresses will be returned.
		/// </summary>
		public List<string> Emails { get; set; }
	}
}
