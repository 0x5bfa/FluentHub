// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A request to deploy a workflow run to an environment.
	/// </summary>
	public class DeploymentRequest
	{
		/// <summary>
		/// Whether or not the current user can approve the deployment
		/// </summary>
		public bool CurrentUserCanApprove { get; set; }

		/// <summary>
		/// The target environment of the deployment
		/// </summary>
		public Environment Environment { get; set; }

		/// <summary>
		/// The teams or users that can review the deployment
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public DeploymentReviewerConnection Reviewers { get; set; }

		/// <summary>
		/// The wait timer in minutes configured in the environment
		/// </summary>
		public int WaitTimer { get; set; }

		/// <summary>
		/// The wait timer in minutes configured in the environment
		/// </summary>
		public DateTimeOffset? WaitTimerStartedAt { get; set; }

		/// <summary>
		/// Humanized string of "The wait timer in minutes configured in the environment"
		/// <summary>
		public string WaitTimerStartedAtHumanized { get; set; }
	}
}
