// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a object that contains package activity statistics such as downloads.
	/// </summary>
	public class PackageStatistics
	{
		/// <summary>
		/// Number of times the package was downloaded since it was created.
		/// </summary>
		public int DownloadsTotalCount { get; set; }
	}
}
