// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Types that can be pinned to a profile page.
	/// </summary>
	public class PinnableItem
	{
		/// <summary>
		/// A Gist.
		/// </summary>
		public Gist Gist { get; set; }

		/// <summary>
		/// A repository contains the content for a project.
		/// </summary>
		public Repository Repository { get; set; }
	}
}
