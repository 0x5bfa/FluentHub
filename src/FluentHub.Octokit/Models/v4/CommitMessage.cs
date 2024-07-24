// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A message to include with a new commit
	/// </summary>
	public class CommitMessage
	{
		/// <summary>
		/// The headline of the message.
		/// </summary>
		public string Headline { get; set; }

		/// <summary>
		/// The body of the message.
		/// </summary>
		public string Body { get; set; }
	}
}
