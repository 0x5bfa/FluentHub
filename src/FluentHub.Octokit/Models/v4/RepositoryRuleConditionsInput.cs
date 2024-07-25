// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Specifies the conditions required for a ruleset to evaluate
	/// </summary>
	public class RepositoryRuleConditionsInput
	{
		/// <summary>
		/// Configuration for the ref_name condition
		/// </summary>
		public RefNameConditionTargetInput RefName { get; set; }

		/// <summary>
		/// Configuration for the repository_name condition
		/// </summary>
		public RepositoryNameConditionTargetInput RepositoryName { get; set; }

		/// <summary>
		/// Configuration for the repository_id condition
		/// </summary>
		public RepositoryIdConditionTargetInput RepositoryId { get; set; }

		/// <summary>
		/// Configuration for the repository_property condition
		/// </summary>
		public RepositoryPropertyConditionTargetInput RepositoryProperty { get; set; }
	}
}
