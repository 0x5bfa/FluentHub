// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The Contributing Guidelines for a repository.
	/// </summary>
	public class ContributingGuidelines
	{
		/// <summary>
		/// The body of the Contributing Guidelines.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// The HTTP path for the Contributing Guidelines.
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for the Contributing Guidelines.
		/// </summary>
		public string Url { get; set; }
	}
}
