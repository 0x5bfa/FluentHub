// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for security advisory connections
	/// </summary>
	public class SecurityAdvisoryOrder
	{
		/// <summary>
		/// The field to order security advisories by.
		/// </summary>
		public SecurityAdvisoryOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
