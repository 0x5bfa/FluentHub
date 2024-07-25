// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a single select field option
	/// </summary>
	public class ProjectV2SingleSelectFieldOptionInput
	{
		/// <summary>
		/// The name of the option
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The display color of the option
		/// </summary>
		public ProjectV2SingleSelectFieldOptionColor Color { get; set; }

		/// <summary>
		/// The description text of the option
		/// </summary>
		public string Description { get; set; }
	}
}
