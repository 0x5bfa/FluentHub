// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ways in which lists of package files can be ordered upon return.
	/// </summary>
	public class PackageFileOrder
	{
		/// <summary>
		/// The field in which to order package files by.
		/// </summary>
		public PackageFileOrderField? Field { get; set; }

		/// <summary>
		/// The direction in which to order package files by the specified field.
		/// </summary>
		public OrderDirection? Direction { get; set; }
	}
}
