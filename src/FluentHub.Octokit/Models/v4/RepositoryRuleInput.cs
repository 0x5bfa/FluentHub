// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Specifies the attributes for a new or updated rule.
	/// </summary>
	public class RepositoryRuleInput
	{
		/// <summary>
		/// Optional ID of this rule when updating
		/// </summary>
		public ID? Id { get; set; }

		/// <summary>
		/// The type of rule to create.
		/// </summary>
		public RepositoryRuleType Type { get; set; }

		/// <summary>
		/// The parameters for the rule.
		/// </summary>
		public RuleParametersInput Parameters { get; set; }
	}
}
