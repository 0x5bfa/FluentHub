// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A deployment review.
	/// </summary>
	public class DeploymentReview
	{
		/// <summary>
		/// The comment the user left.
		/// </summary>
		public string Comment { get; set; }

		/// <summary>
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		/// <summary>
		/// The environments approved or rejected
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public EnvironmentConnection Environments { get; set; }

		/// <summary>
		/// The Node ID of the DeploymentReview object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The decision of the user.
		/// </summary>
		public DeploymentReviewState State { get; set; }

		/// <summary>
		/// The user that reviewed the deployment.
		/// </summary>
		public User User { get; set; }
	}
}
