// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a single highlight in a search result match.
	/// </summary>
	public class TextMatchHighlight
	{
		/// <summary>
		/// The indice in the fragment where the matched text begins.
		/// </summary>
		public int BeginIndice { get; set; }

		/// <summary>
		/// The indice in the fragment where the matched text ends.
		/// </summary>
		public int EndIndice { get; set; }

		/// <summary>
		/// The text matched.
		/// </summary>
		public string Text { get; set; }
	}
}
