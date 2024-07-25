// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for repository connections
	/// </summary>
	public class RepositoryOrder
	{
		/// <summary>
		/// The field to order repositories by.
		/// </summary>
		public RepositoryOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
