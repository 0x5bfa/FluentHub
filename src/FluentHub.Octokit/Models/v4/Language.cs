// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a given language found in repositories.
	/// </summary>
	public class Language
	{
		/// <summary>
		/// The color defined for the current language.
		/// </summary>
		public string Color { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// The name of the current language.
		/// </summary>
		public string Name { get; set; }
	}
}
