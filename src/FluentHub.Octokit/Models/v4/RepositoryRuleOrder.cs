// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for repository rules.
	/// </summary>
	public class RepositoryRuleOrder
	{
		/// <summary>
		/// The field to order repository rules by.
		/// </summary>
		public RepositoryRuleOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
