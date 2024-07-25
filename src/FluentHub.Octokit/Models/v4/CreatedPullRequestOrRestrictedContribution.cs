// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents either a pull request the viewer can access or a restricted contribution.
	/// </summary>
	public class CreatedPullRequestOrRestrictedContribution
	{
		/// <summary>
		/// Represents the contribution a user made on GitHub by opening a pull request.
		/// </summary>
		public CreatedPullRequestContribution CreatedPullRequestContribution { get; set; }

		/// <summary>
		/// Represents a private contribution a user made on GitHub.
		/// </summary>
		public RestrictedContribution RestrictedContribution { get; set; }
	}
}
