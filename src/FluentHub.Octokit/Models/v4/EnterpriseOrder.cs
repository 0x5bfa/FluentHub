// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for enterprises.
	/// </summary>
	public class EnterpriseOrder
	{
		/// <summary>
		/// The field to order enterprises by.
		/// </summary>
		public EnterpriseOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
