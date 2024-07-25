// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Iteration field iteration settings for a project.
	/// </summary>
	public class ProjectV2IterationFieldIteration
	{
		/// <summary>
		/// The iteration's duration in days
		/// </summary>
		public int Duration { get; set; }

		/// <summary>
		/// The iteration's ID.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// The iteration's start date
		/// </summary>
		public string StartDate { get; set; }

		/// <summary>
		/// The iteration's title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// The iteration's html title.
		/// </summary>
		public string TitleHTML { get; set; }
	}
}
