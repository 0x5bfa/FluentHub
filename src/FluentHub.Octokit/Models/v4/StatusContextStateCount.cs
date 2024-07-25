// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a count of the state of a status context.
	/// </summary>
	public class StatusContextStateCount
	{
		/// <summary>
		/// The number of statuses with this state.
		/// </summary>
		public int Count { get; set; }

		/// <summary>
		/// The state of a status context.
		/// </summary>
		public StatusState State { get; set; }
	}
}
