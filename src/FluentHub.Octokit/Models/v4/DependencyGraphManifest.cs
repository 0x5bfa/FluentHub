// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Dependency manifest for a repository
	/// </summary>
	public class DependencyGraphManifest
	{
		/// <summary>
		/// Path to view the manifest file blob
		/// </summary>
		public string BlobPath { get; set; }

		/// <summary>
		/// A list of manifest dependencies
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public DependencyGraphDependencyConnection Dependencies { get; set; }

		/// <summary>
		/// The number of dependencies listed in the manifest
		/// </summary>
		public int? DependenciesCount { get; set; }

		/// <summary>
		/// Is the manifest too big to parse?
		/// </summary>
		public bool ExceedsMaxSize { get; set; }

		/// <summary>
		/// Fully qualified manifest filename
		/// </summary>
		public string Filename { get; set; }

		/// <summary>
		/// The Node ID of the DependencyGraphManifest object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Were we able to parse the manifest?
		/// </summary>
		public bool Parseable { get; set; }

		/// <summary>
		/// The repository containing the manifest
		/// </summary>
		public Repository Repository { get; set; }
	}
}
