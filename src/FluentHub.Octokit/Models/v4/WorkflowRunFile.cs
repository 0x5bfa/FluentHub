// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An executed workflow file for a workflow run.
	/// </summary>
	public class WorkflowRunFile
	{
		/// <summary>
		/// The Node ID of the WorkflowRunFile object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The path of the workflow file relative to its repository.
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// The direct link to the file in the repository which stores the workflow file.
		/// </summary>
		public string RepositoryFileUrl { get; set; }

		/// <summary>
		/// The repository name and owner which stores the workflow file.
		/// </summary>
		public string RepositoryName { get; set; }

		/// <summary>
		/// The HTTP path for this workflow run file
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// The parent workflow run execution for this file.
		/// </summary>
		public WorkflowRun Run { get; set; }

		/// <summary>
		/// The HTTP URL for this workflow run file
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// If the viewer has permissions to push to the repository which stores the workflow.
		/// </summary>
		public bool ViewerCanPushRepository { get; set; }

		/// <summary>
		/// If the viewer has permissions to read the repository which stores the workflow.
		/// </summary>
		public bool ViewerCanReadRepository { get; set; }
	}
}
