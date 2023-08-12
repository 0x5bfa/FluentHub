// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ways in which to filter lists of projects.
	/// </summary>
	public class ProjectV2Filters
	{
		/// <summary>
		/// List project v2 filtered by the state given.
		/// </summary>
		public ProjectV2State? State { get; set; }
	}
}
