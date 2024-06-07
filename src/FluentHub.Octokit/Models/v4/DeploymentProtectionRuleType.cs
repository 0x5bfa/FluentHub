// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible protection rule types.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DeploymentProtectionRuleType
	{
		/// <summary>
		/// Required reviewers
		/// </summary>
		[EnumMember(Value = "REQUIRED_REVIEWERS")]
		RequiredReviewers,

		/// <summary>
		/// Wait timer
		/// </summary>
		[EnumMember(Value = "WAIT_TIMER")]
		WaitTimer,

		/// <summary>
		/// Branch policy
		/// </summary>
		[EnumMember(Value = "BRANCH_POLICY")]
		BranchPolicy,
	}
}
