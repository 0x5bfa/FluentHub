// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Parameters to be used for the repository_id condition
	/// </summary>
	public class RepositoryIdConditionTarget
	{
		/// <summary>
		/// One of these repo IDs must match the repo.
		/// </summary>
		public List<ID> RepositoryIds { get; set; }
	}
}
