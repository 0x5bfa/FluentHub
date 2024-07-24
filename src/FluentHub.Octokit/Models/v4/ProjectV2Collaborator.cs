// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A collaborator to update on a project. Only one of the userId or teamId should be provided.
	/// </summary>
	public class ProjectV2Collaborator
	{
		/// <summary>
		/// The ID of the user as a collaborator.
		/// </summary>
		public ID? UserId { get; set; }

		/// <summary>
		/// The ID of the team as a collaborator.
		/// </summary>
		public ID? TeamId { get; set; }

		/// <summary>
		/// The role to grant the collaborator
		/// </summary>
		public ProjectV2Roles Role { get; set; }
	}
}
