// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Types that can be inside a StatusCheckRollup context.
	/// </summary>
	public class StatusCheckRollupContext
	{
		/// <summary>
		/// A check run.
		/// </summary>
		public CheckRun CheckRun { get; set; }

		/// <summary>
		/// Represents an individual commit status context
		/// </summary>
		public StatusContext StatusContext { get; set; }
	}
}
