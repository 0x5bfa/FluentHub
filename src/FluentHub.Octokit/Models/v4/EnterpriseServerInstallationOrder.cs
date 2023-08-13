// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for Enterprise Server installation connections.
	/// </summary>
	public class EnterpriseServerInstallationOrder
	{
		/// <summary>
		/// The field to order Enterprise Server installations by.
		/// </summary>
		public EnterpriseServerInstallationOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
