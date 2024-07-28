// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A workflow that must run for this rule to pass
	/// </summary>
	public class WorkflowFileReferenceInput
	{
		/// <summary>
		/// The path to the workflow file
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// The ref (branch or tag) of the workflow file to use
		/// </summary>
		public string Ref { get; set; }

		/// <summary>
		/// The ID of the repository where the workflow is defined
		/// </summary>
		public int RepositoryId { get; set; }

		/// <summary>
		/// The commit SHA of the workflow file to use
		/// </summary>
		public string Sha { get; set; }
	}
}
