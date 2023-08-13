// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Set of conditions that determine if a ruleset will evaluate
	/// </summary>
	public class RepositoryRuleConditions
	{
		/// <summary>
		/// Configuration for the ref_name condition
		/// </summary>
		public RefNameConditionTarget RefName { get; set; }

		/// <summary>
		/// Configuration for the repository_id condition
		/// </summary>
		public RepositoryIdConditionTarget RepositoryId { get; set; }

		/// <summary>
		/// Configuration for the repository_name condition
		/// </summary>
		public RepositoryNameConditionTarget RepositoryName { get; set; }
	}
}
