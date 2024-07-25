// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// Recent projects for the owner.
	/// </summary>
	public interface IProjectV2Recent
	{
		/// <summary>
		/// Recent projects that this user has modified in the context of the owner.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		ProjectV2Connection RecentProjects { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class ProjectV2Recent : IProjectV2Recent
	{
		public ProjectV2Connection RecentProjects { get; set; }
	}
}

