// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A workflow contains meta information about an Actions workflow file.
	/// </summary>
	public class Workflow
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
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// The name of the workflow.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The HTTP path for this workflow
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// The runs of the workflow.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for the connection</param>
		public WorkflowRunConnection Runs { get; set; }

		/// <summary>
		/// The state of the workflow.
		/// </summary>
		public WorkflowState State { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }

		/// <summary>
		/// The HTTP URL for this workflow
		/// </summary>
		public string Url { get; set; }
	}
}
