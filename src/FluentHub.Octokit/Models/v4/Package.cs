// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Information for an uploaded package.
	/// </summary>
	public class Package
	{
		/// <summary>
		/// The Node ID of the Package object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Find the latest version for the package.
		/// </summary>
		public PackageVersion LatestVersion { get; set; }

		/// <summary>
		/// Identifies the name of the package.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Identifies the type of the package.
		/// </summary>
		public PackageType PackageType { get; set; }

		/// <summary>
		/// The repository this package belongs to.
		/// </summary>
		public Repository Repository { get; set; }

		/// <summary>
		/// Statistics about package activity.
		/// </summary>
		public PackageStatistics Statistics { get; set; }

		/// <summary>
		/// Find package version by version string.
		/// </summary>
		/// <param name="version">The package version.</param>
		public PackageVersion Version { get; set; }

		/// <summary>
		/// list of versions for this package
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering of the returned packages.</param>
		public PackageVersionConnection Versions { get; set; }
	}
}
