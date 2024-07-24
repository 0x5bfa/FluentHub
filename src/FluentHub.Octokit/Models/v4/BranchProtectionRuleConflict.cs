// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A conflict between two branch protection rules.
	/// </summary>
	public class BranchProtectionRuleConflict
	{
		/// <summary>
		/// Identifies the branch protection rule.
		/// </summary>
		public BranchProtectionRule BranchProtectionRule { get; set; }

		/// <summary>
		/// Identifies the conflicting branch protection rule.
		/// </summary>
		public BranchProtectionRule ConflictingBranchProtectionRule { get; set; }

		/// <summary>
		/// Identifies the branch ref that has conflicting rules
		/// </summary>
		public Ref Ref { get; set; }
	}
}
