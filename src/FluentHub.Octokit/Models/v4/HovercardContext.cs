// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// An individual line of a hovercard
	/// </summary>
	public interface IHovercardContext
	{
		/// <summary>
		/// A string describing this context
		/// </summary>
		string Message { get; set; }

		/// <summary>
		/// An octicon to accompany this context
		/// </summary>
		string Octicon { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class HovercardContext : IHovercardContext
	{
		public string Message { get; set; }

		public string Octicon { get; set; }
	}
}

