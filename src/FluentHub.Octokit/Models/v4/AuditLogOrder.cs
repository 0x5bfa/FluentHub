// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for Audit Log connections.
	/// </summary>
	public class AuditLogOrder
	{
		/// <summary>
		/// The field to order Audit Logs by.
		/// </summary>
		public AuditLogOrderField? Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection? Direction { get; set; }
	}
}
