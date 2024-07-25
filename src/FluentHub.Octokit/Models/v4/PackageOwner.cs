// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// Represents an owner of a package.
	/// </summary>
	public interface IPackageOwner
	{
		/// <summary>
		/// The Node ID of the PackageOwner object
		/// </summary>
		ID Id { get; set; }

		/// <summary>
		/// A list of packages under the owner.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="names">Find packages by their names.</param>
		/// <param name="orderBy">Ordering of the returned packages.</param>
		/// <param name="packageType">Filter registry package by type.</param>
		/// <param name="repositoryId">Find packages in a repository by ID.</param>
		PackageConnection Packages { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class PackageOwner : IPackageOwner
	{
		public ID Id { get; set; }

		public PackageConnection Packages { get; set; }
	}
}

