// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A team, user, or app who has the ability to push to a protected branch.
	/// </summary>
	public class PushAllowance
	{
		/// <summary>
		/// The actor that can push.
		/// </summary>
		public PushAllowanceActor Actor { get; set; }

		/// <summary>
		/// Identifies the branch protection rule associated with the allowed user, team, or app.
		/// </summary>
		public BranchProtectionRule BranchProtectionRule { get; set; }

		public ID Id { get; set; }
	}
}
