// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A repository-topic connects a repository to a topic.
	/// </summary>
	public class RepositoryTopic
	{
		/// <summary>
		/// The Node ID of the RepositoryTopic object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The HTTP path for this repository-topic.
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// The topic.
		/// </summary>
		public Topic Topic { get; set; }

		/// <summary>
		/// The HTTP URL for this repository-topic.
		/// </summary>
		public string Url { get; set; }
	}
}
