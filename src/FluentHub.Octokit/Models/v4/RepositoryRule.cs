// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A repository rule.
	/// </summary>
	public class RepositoryRule
	{
		/// <summary>
		/// The Node ID of the RepositoryRule object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The parameters for this rule.
		/// </summary>
		public RuleParameters Parameters { get; set; }

		/// <summary>
		/// The repository ruleset associated with this rule configuration
		/// </summary>
		public RepositoryRuleset RepositoryRuleset { get; set; }

		/// <summary>
		/// The type of rule.
		/// </summary>
		public RepositoryRuleType Type { get; set; }
	}
}
