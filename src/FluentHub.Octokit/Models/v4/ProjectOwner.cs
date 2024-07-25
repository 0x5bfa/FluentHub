// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// Represents an owner of a Project.
	/// </summary>
	public interface IProjectOwner
	{
		/// <summary>
		/// The Node ID of the ProjectOwner object
		/// </summary>
		ID Id { get; set; }

		/// <summary>
		/// Find project by number.
		/// </summary>
		/// <param name="number">The project number to find.</param>
		Project Project { get; set; }

		/// <summary>
		/// A list of projects under the owner.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for projects returned from the connection</param>
		/// <param name="search">Query to search projects by, currently only searching by name.</param>
		/// <param name="states">A list of states to filter the projects by.</param>
		ProjectConnection Projects { get; set; }

		/// <summary>
		/// The HTTP path listing owners projects
		/// </summary>
		string ProjectsResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL listing owners projects
		/// </summary>
		string ProjectsUrl { get; set; }

		/// <summary>
		/// Can the current viewer create new projects on this owner.
		/// </summary>
		bool ViewerCanCreateProjects { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class ProjectOwner : IProjectOwner
	{
		public ID Id { get; set; }

		public Project Project { get; set; }

		public ProjectConnection Projects { get; set; }

		public string ProjectsResourcePath { get; set; }

		public string ProjectsUrl { get; set; }

		public bool ViewerCanCreateProjects { get; set; }
	}
}

