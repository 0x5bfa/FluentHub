// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated input type of DequeuePullRequest
	/// </summary>
	public class DequeuePullRequestInput
	{
		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }

		/// <summary>
		/// The ID of the pull request to be dequeued.
		/// </summary>
		public ID Id { get; set; }
	}
}
