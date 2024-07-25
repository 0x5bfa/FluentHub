// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for Enterprise Server user account email connections.
	/// </summary>
	public class EnterpriseServerUserAccountEmailOrder
	{
		/// <summary>
		/// The field to order emails by.
		/// </summary>
		public EnterpriseServerUserAccountEmailOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
