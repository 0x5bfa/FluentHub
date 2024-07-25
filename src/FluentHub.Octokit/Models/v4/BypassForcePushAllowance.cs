// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A user, team, or app who has the ability to bypass a force push requirement on a protected branch.
	/// </summary>
	public class BypassForcePushAllowance
	{
		/// <summary>
		/// The actor that can force push.
		/// </summary>
		public BranchActorAllowanceActor Actor { get; set; }

		/// <summary>
		/// Identifies the branch protection rule associated with the allowed user, team, or app.
		/// </summary>
		public BranchProtectionRule BranchProtectionRule { get; set; }

		/// <summary>
		/// The Node ID of the BypassForcePushAllowance object
		/// </summary>
		public ID Id { get; set; }
	}
}
