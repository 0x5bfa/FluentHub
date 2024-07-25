// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for Enterprise Server user accounts upload connections.
	/// </summary>
	public class EnterpriseServerUserAccountsUploadOrder
	{
		/// <summary>
		/// The field to order user accounts uploads by.
		/// </summary>
		public EnterpriseServerUserAccountsUploadOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
