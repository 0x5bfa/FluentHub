// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Detail needed to display a hovercard for a user
	/// </summary>
	public class Hovercard
	{
		/// <summary>
		/// Each of the contexts for this hovercard
		/// </summary>
		public List<IHovercardContext> Contexts { get; set; }
	}
}
