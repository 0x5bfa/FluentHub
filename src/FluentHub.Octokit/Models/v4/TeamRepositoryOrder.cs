// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for team repository connections
	/// </summary>
	public class TeamRepositoryOrder
	{
		/// <summary>
		/// The field to order repositories by.
		/// </summary>
		public TeamRepositoryOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
