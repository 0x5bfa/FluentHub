// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The targets supported for rulesets
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
	}
}
