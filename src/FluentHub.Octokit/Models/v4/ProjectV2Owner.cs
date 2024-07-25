// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// Represents an owner of a project.
	/// </summary>
	public interface IProjectV2Owner
	{
		/// <summary>
		/// The Node ID of the ProjectV2Owner object
		/// </summary>
		ID Id { get; set; }

		/// <summary>
		/// Find a project by number.
		/// </summary>
		/// <param name="number">The project number.</param>
		ProjectV2 ProjectV2 { get; set; }

		/// <summary>
		/// A list of projects under the owner.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">How to order the returned projects.</param>
		/// <param name="query">A project to search for under the the owner.</param>
		ProjectV2Connection ProjectsV2 { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class ProjectV2Owner : IProjectV2Owner
	{
		public ID Id { get; set; }

		public ProjectV2 ProjectV2 { get; set; }

		public ProjectV2Connection ProjectsV2 { get; set; }
	}
}

