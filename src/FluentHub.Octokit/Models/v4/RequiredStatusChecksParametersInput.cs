// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Choose which status checks must pass before the ref is updated. When enabled, commits must first be pushed to another ref where the checks pass.
	/// </summary>
	public class RequiredStatusChecksParametersInput
	{
		/// <summary>
		/// Status checks that are required.
		/// </summary>
		public List<StatusCheckConfigurationInput> RequiredStatusChecks { get; set; }

		/// <summary>
		/// Whether pull requests targeting a matching branch must be tested with the latest code. This setting will not take effect unless at least one status check is enabled.
		/// </summary>
		public bool StrictRequiredStatusChecksPolicy { get; set; }
	}
}
