// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Information about pagination in a connection.
	/// </summary>
	public class PageInfo
	{
		/// <summary>
		/// When paginating forwards, the cursor to continue.
		/// </summary>
		public string EndCursor { get; set; }

		/// <summary>
		/// When paginating forwards, are there more items?
		/// </summary>
		public bool HasNextPage { get; set; }

		/// <summary>
		/// When paginating backwards, are there more items?
		/// </summary>
		public bool HasPreviousPage { get; set; }

		/// <summary>
		/// When paginating backwards, the cursor to continue.
		/// </summary>
		public string StartCursor { get; set; }
	}
}
