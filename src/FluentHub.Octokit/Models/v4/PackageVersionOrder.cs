// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ways in which lists of package versions can be ordered upon return.
	/// </summary>
	public class PackageVersionOrder
	{
		/// <summary>
		/// The field in which to order package versions by.
		/// </summary>
		public PackageVersionOrderField? Field { get; set; }

		/// <summary>
		/// The direction in which to order package versions by the specified field.
		/// </summary>
		public OrderDirection? Direction { get; set; }
	}
}
