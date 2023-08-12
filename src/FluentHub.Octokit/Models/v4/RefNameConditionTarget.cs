// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Parameters to be used for the ref_name condition
	/// </summary>
	public class RefNameConditionTarget
	{
		/// <summary>
		/// Array of ref names or patterns to exclude. The condition will not pass if any of these patterns match.
		/// </summary>
		public List<string> Exclude { get; set; }

		/// <summary>
		/// Array of ref names or patterns to include. One of these patterns must match for the condition to pass. Also accepts `~DEFAULT_BRANCH` to include the default branch or `~ALL` to include all branches.
		/// </summary>
		public List<string> Include { get; set; }
	}
}
