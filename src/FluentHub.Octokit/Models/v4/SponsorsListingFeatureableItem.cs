// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A record that can be featured on a GitHub Sponsors profile.
	/// </summary>
	public class SponsorsListingFeatureableItem
	{
		/// <summary>
		/// A repository contains the content for a project.
		/// </summary>
		public Repository Repository { get; set; }

		/// <summary>
		/// A user is an individual's account on GitHub that owns repositories and can make new content.
		/// </summary>
		public User User { get; set; }
	}
}
