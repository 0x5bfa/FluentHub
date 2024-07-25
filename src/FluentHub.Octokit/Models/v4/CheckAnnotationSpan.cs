// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An inclusive pair of positions for a check annotation.
	/// </summary>
	public class CheckAnnotationSpan
	{
		/// <summary>
		/// End position (inclusive).
		/// </summary>
		public CheckAnnotationPosition End { get; set; }

		/// <summary>
		/// Start position (inclusive).
		/// </summary>
		public CheckAnnotationPosition Start { get; set; }
	}
}
