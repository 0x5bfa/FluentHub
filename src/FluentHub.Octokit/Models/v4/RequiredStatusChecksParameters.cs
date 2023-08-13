// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Choose which status checks must pass before branches can be merged into a branch that matches this rule. When enabled, commits must first be pushed to another branch, then merged or pushed directly to a branch that matches this rule after status checks have passed.
	/// </summary>
	public class RequiredStatusChecksParameters
	{
		/// <summary>
		/// Status checks that are required.
		/// </summary>
		public List<StatusCheckConfiguration> RequiredStatusChecks { get; set; }

		/// <summary>
		/// Whether pull requests targeting a matching branch must be tested with the latest code. This setting will not take effect unless at least one status check is enabled.
		/// </summary>
		public bool StrictRequiredStatusChecksPolicy { get; set; }
	}
}
