// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Types which can be actors for `BranchActorAllowance` objects.
	/// </summary>
	public class BranchActorAllowanceActor
	{
		/// <summary>
		/// A GitHub App.
		/// </summary>
		public App App { get; set; }

		/// <summary>
		/// A team of users in an organization.
		/// </summary>
		public Team Team { get; set; }

		/// <summary>
		/// A user is an individual's account on GitHub that owns repositories and can make new content.
		/// </summary>
		public User User { get; set; }
	}
}
