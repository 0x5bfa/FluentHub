// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Single select field option for a configuration for a project.
	/// </summary>
	public class ProjectV2SingleSelectFieldOption
	{
		/// <summary>
		/// The option's display color.
		/// </summary>
		public ProjectV2SingleSelectFieldOptionColor Color { get; set; }

		/// <summary>
		/// The option's plain-text description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The option's description, possibly containing HTML.
		/// </summary>
		public string DescriptionHTML { get; set; }

		/// <summary>
		/// The option's ID.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// The option's name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The option's html name.
		/// </summary>
		public string NameHTML { get; set; }
	}
}
