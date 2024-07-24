// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a object that contains package version activity statistics such as downloads.
	/// </summary>
	public class PackageVersionStatistics
	{
		/// <summary>
		/// Number of times the package was downloaded since it was created.
		/// </summary>
		public int DownloadsTotalCount { get; set; }
	}
}
