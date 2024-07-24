// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A generic hovercard context with a message and icon
	/// </summary>
	public class GenericHovercardContext
	{
		/// <summary>
		/// A string describing this context
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// An octicon to accompany this context
		/// </summary>
		public string Octicon { get; set; }
	}
}
