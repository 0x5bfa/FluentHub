// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A dependency manifest entry
	/// </summary>
	public class DependencyGraphDependency
	{
		/// <summary>
		/// Does the dependency itself have dependencies?
		/// </summary>
		public bool HasDependencies { get; set; }

		/// <summary>
		/// The original name of the package, as it appears in the manifest.
		/// </summary>
		[Obsolete(@"`packageLabel` will be removed. Use normalized `packageName` field instead. Removal on 2022-10-01 UTC.")]
		public string PackageLabel { get; set; }

		/// <summary>
		/// The dependency package manager
		/// </summary>
		public string PackageManager { get; set; }

		/// <summary>
		/// The name of the package in the canonical form used by the package manager.
		/// </summary>
		public string PackageName { get; set; }

		/// <summary>
		/// The repository containing the package
		/// </summary>
		public Repository Repository { get; set; }

		/// <summary>
		/// The dependency version requirements
		/// </summary>
		public string Requirements { get; set; }
	}
}
