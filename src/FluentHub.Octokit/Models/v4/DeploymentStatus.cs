// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Describes the status of a given deployment attempt.
	/// </summary>
	public class DeploymentStatus
	{
		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// Identifies the actor who triggered the deployment.
		/// </summary>
		public IActor Creator { get; set; }

		/// <summary>
		/// Identifies the deployment associated with status.
		/// </summary>
		public Deployment Deployment { get; set; }

		/// <summary>
		/// Identifies the description of the deployment.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Identifies the environment of the deployment at the time of this deployment status
		/// </summary>
		public string Environment { get; set; }

		/// <summary>
		/// Identifies the environment URL of the deployment.
		/// </summary>
		public string EnvironmentUrl { get; set; }

		/// <summary>
		/// The Node ID of the DeploymentStatus object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Identifies the log URL of the deployment.
		/// </summary>
		public string LogUrl { get; set; }

		/// <summary>
		/// Identifies the current state of the deployment.
		/// </summary>
		public DeploymentStatusState State { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }
	}
}
