// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for mannequins.
	/// </summary>
	public class MannequinOrder
	{
		/// <summary>
		/// The field to order mannequins by.
		/// </summary>
		public MannequinOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
