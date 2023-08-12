// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Types that can represent a repository ruleset bypass actor.
	/// </summary>
	public class BypassActor
	{
		/// <summary>
		/// A GitHub App.
		/// </summary>
		public App App { get; set; }

		/// <summary>
		/// A team of users in an organization.
		/// </summary>
		public Team Team { get; set; }
	}
}
