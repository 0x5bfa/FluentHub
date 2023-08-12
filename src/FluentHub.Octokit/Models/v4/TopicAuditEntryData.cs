// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// Metadata for an audit entry with a topic.
	/// </summary>
	public interface ITopicAuditEntryData
	{
		/// <summary>
		/// The name of the topic added to the repository
		/// </summary>
		Topic Topic { get; set; }

		/// <summary>
		/// The name of the topic added to the repository
		/// </summary>
		string TopicName { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class TopicAuditEntryData : ITopicAuditEntryData
	{
		public Topic Topic { get; set; }

		public string TopicName { get; set; }
	}
}

