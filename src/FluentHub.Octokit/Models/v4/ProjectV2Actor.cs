// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Possible collaborators for a project.
	/// </summary>
	public class ProjectV2Actor
	{
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
