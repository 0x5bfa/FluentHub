// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Types that can grant permissions on a repository to a user
	/// </summary>
	public class PermissionGranter
	{
		/// <summary>
		/// An account on GitHub, with one or more owners, that has repositories, members and teams.
		/// </summary>
		public Organization Organization { get; set; }

		/// <summary>
		/// A repository contains the content for a project.
		/// </summary>
		public Repository Repository { get; set; }

		/// <summary>
		/// A team of users in an organization.
		/// </summary>
		public Team Team { get; set; }
	}
}
