// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A version tag contains the mapping between a tag name and a version.
	/// </summary>
	public class PackageTag
	{
		/// <summary>
		/// The Node ID of the PackageTag object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Identifies the tag name of the version.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Version that the tag is associated with.
		/// </summary>
		public PackageVersion Version { get; set; }
	}
}
