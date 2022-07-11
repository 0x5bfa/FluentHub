namespace FluentHub.Octokit.v4.Model
{
    using System;
    using System.Collections.Generic;

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

namespace FluentHub.Octokit.v4.Model.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubITopicAuditEntryData
    {
        

        public Topic Topic { get; set; }

        public string TopicName { get; set; }
    }
}