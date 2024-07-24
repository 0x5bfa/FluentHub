// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Choose which tools must provide code scanning results before the reference is updated. When configured, code scanning must be enabled and have results for both the commit and the reference being updated.
	/// </summary>
	public class CodeScanningParameters
	{
		/// <summary>
		/// Tools that must provide code scanning results for this rule to pass.
		/// </summary>
		public List<CodeScanningTool> CodeScanningTools { get; set; }
	}
}
