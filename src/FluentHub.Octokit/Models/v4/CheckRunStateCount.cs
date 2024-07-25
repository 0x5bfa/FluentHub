// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a count of the state of a check run.
	/// </summary>
	public class CheckRunStateCount
	{
		/// <summary>
		/// The number of check runs with this state.
		/// </summary>
		public int Count { get; set; }

		/// <summary>
		/// The state of a check run.
		/// </summary>
		public CheckRunState State { get; set; }
	}
}
