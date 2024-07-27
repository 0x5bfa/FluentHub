// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// Entities that can be minimized.
	/// </summary>
	public interface IMinimizable
	{
		/// <summary>
		/// Returns whether or not a comment has been minimized.
		/// </summary>
		bool IsMinimized { get; set; }

		/// <summary>
		/// Returns why the comment was minimized. One of `abuse`, `off-topic`, `outdated`, `resolved`, `duplicate` and `spam`. Note that the case and formatting of these values differs from the inputs to the `MinimizeComment` mutation.
		/// </summary>
		string MinimizedReason { get; set; }

		/// <summary>
		/// Check if the current viewer can minimize this object.
		/// </summary>
		bool ViewerCanMinimize { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class Minimizable : IMinimizable
	{
		public bool IsMinimized { get; set; }

		public string MinimizedReason { get; set; }

		public bool ViewerCanMinimize { get; set; }
	}
}

