// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A check run.
	/// </summary>
	public class CheckRun
	{
		/// <summary>
		/// The check run's annotations
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public CheckAnnotationConnection Annotations { get; set; }

		/// <summary>
		/// The check suite that this run is a part of.
		/// </summary>
		public CheckSuite CheckSuite { get; set; }

		/// <summary>
		/// Identifies the date and time when the check run was completed.
		/// </summary>
		public DateTimeOffset? CompletedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the check run was completed."
		/// <summary>
		public string CompletedAtHumanized { get; set; }

		/// <summary>
		/// The conclusion of the check run.
		/// </summary>
		public CheckConclusionState? Conclusion { get; set; }

		/// <summary>
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		/// <summary>
		/// The corresponding deployment for this job, if any
		/// </summary>
		public Deployment Deployment { get; set; }

		/// <summary>
		/// The URL from which to find full details of the check run on the integrator's site.
		/// </summary>
		public string DetailsUrl { get; set; }

		/// <summary>
		/// A reference for the check run on the integrator's system.
		/// </summary>
		public string ExternalId { get; set; }

		/// <summary>
		/// The Node ID of the CheckRun object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Whether this is required to pass before merging for a specific pull request.
		/// </summary>
		/// <param name="pullRequestId">The id of the pull request this is required for</param>
		/// <param name="pullRequestNumber">The number of the pull request this is required for</param>
		public bool IsRequired { get; set; }

		/// <summary>
		/// The name of the check for this check run.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Information about a pending deployment, if any, in this check run
		/// </summary>
		public DeploymentRequest PendingDeploymentRequest { get; set; }

		/// <summary>
		/// The permalink to the check run summary.
		/// </summary>
		public string Permalink { get; set; }

		/// <summary>
		/// The repository associated with this check run.
		/// </summary>
		public Repository Repository { get; set; }

		/// <summary>
		/// The HTTP path for this check run.
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// Identifies the date and time when the check run was started.
		/// </summary>
		public DateTimeOffset? StartedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the check run was started."
		/// <summary>
		public string StartedAtHumanized { get; set; }

		/// <summary>
		/// The current status of the check run.
		/// </summary>
		public CheckStatusState Status { get; set; }

		/// <summary>
		/// The check run's steps
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="number">Step number</param>
		public CheckStepConnection Steps { get; set; }

		/// <summary>
		/// A string representing the check run's summary
		/// </summary>
		public string Summary { get; set; }

		/// <summary>
		/// A string representing the check run's text
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// A string representing the check run
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// The HTTP URL for this check run.
		/// </summary>
		public string Url { get; set; }
	}
}
