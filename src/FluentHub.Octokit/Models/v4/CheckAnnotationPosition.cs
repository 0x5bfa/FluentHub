// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A character position in a check annotation.
	/// </summary>
	public class CheckAnnotationPosition
	{
		/// <summary>
		/// Column number (1 indexed).
		/// </summary>
		public int? Column { get; set; }

		/// <summary>
		/// Line number (1 indexed).
		/// </summary>
		public int Line { get; set; }
	}
}
