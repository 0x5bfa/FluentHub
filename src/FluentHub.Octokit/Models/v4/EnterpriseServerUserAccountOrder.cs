// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for Enterprise Server user account connections.
	/// </summary>
	public class EnterpriseServerUserAccountOrder
	{
		/// <summary>
		/// The field to order user accounts by.
		/// </summary>
		public EnterpriseServerUserAccountOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
