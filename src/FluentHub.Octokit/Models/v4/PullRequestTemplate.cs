// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A repository pull request template.
	/// </summary>
	public class PullRequestTemplate
	{
		/// <summary>
		/// The body of the template
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// The filename of the template
		/// </summary>
		public string Filename { get; set; }

		/// <summary>
		/// The repository the template belongs to
		/// </summary>
		public Repository Repository { get; set; }
	}
}
