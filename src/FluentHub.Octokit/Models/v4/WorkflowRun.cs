// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A workflow run.
	/// </summary>
	public class WorkflowRun
	{
		/// <summary>
		/// The check suite this workflow run belongs to.
		/// </summary>
		public CheckSuite CheckSuite { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		/// <summary>
		/// The log of deployment reviews
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public DeploymentReviewConnection DeploymentReviews { get; set; }

		/// <summary>
		/// The event that triggered the workflow run
		/// </summary>
		public string Event { get; set; }

		/// <summary>
		/// The workflow file
		/// </summary>
		public WorkflowRunFile File { get; set; }

		/// <summary>
		/// The Node ID of the WorkflowRun object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The pending deployment requests of all check runs in this workflow run
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public DeploymentRequestConnection PendingDeploymentRequests { get; set; }

		/// <summary>
		/// The HTTP path for this workflow run
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// A number that uniquely identifies this workflow run in its parent workflow.
		/// </summary>
		public int RunNumber { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }

		/// <summary>
		/// The HTTP URL for this workflow run
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// The workflow executed in this workflow run.
		/// </summary>
		public Workflow Workflow { get; set; }
	}
}
