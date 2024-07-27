// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for verifiable domain connections.
	/// </summary>
	public class VerifiableDomainOrder
	{
		/// <summary>
		/// The field to order verifiable domains by.
		/// </summary>
		public VerifiableDomainOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
