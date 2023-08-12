// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents the language of a repository.
	/// </summary>
	public class LanguageEdge
	{
		public string Cursor { get; set; }

		public Language Node { get; set; }

		/// <summary>
		/// The number of bytes of code written in the language.
		/// </summary>
		public int Size { get; set; }
	}
}
