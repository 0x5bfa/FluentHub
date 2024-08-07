// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated return type of CreateLinkedBranch
	/// </summary>
	public class CreateLinkedBranchPayload
	{
		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }

		/// <summary>
		/// The issue that was linked to.
		/// </summary>
		public Issue Issue { get; set; }

		/// <summary>
		/// The new branch issue reference.
		/// </summary>
		public LinkedBranch LinkedBranch { get; set; }
	}
}
