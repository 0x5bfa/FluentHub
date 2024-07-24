// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The targets supported for rulesets. NOTE: The push target is in beta and subject to change.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RepositoryRulesetTarget
	{
		/// <summary>
		/// Branch
		/// </summary>
		[EnumMember(Value = "BRANCH")]
		Branch,

		/// <summary>
		/// Tag
		/// </summary>
		[EnumMember(Value = "TAG")]
		Tag,

		/// <summary>
		/// Push
		/// </summary>
		[EnumMember(Value = "PUSH")]
		Push,
	}
}
