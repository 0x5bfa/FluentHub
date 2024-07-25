// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The bypass mode for a specific actor on a ruleset.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RepositoryRulesetBypassActorBypassMode
	{
		/// <summary>
		/// The actor can always bypass rules
		/// </summary>
		[EnumMember(Value = "ALWAYS")]
		Always,

		/// <summary>
		/// The actor can only bypass rules via a pull request
		/// </summary>
		[EnumMember(Value = "PULL_REQUEST")]
		PullRequest,
	}
}
