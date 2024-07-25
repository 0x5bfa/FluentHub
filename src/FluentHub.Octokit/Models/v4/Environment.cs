// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An environment.
	/// </summary>
	public class Environment
	{
		/// <summary>
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		/// <summary>
		/// The Node ID of the Environment object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Indicates whether or not this environment is currently pinned to the repository
		/// </summary>
		public bool? IsPinned { get; set; }

		/// <summary>
		/// The latest completed deployment with status success, failure, or error if it exists
		/// </summary>
		public Deployment LatestCompletedDeployment { get; set; }

		/// <summary>
		/// The name of the environment
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The position of the environment if it is pinned, null if it is not pinned
		/// </summary>
		public int? PinnedPosition { get; set; }

		/// <summary>
		/// The protection rules defined for this environment
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public DeploymentProtectionRuleConnection ProtectionRules { get; set; }
	}
}
