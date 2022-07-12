namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Metadata for an audit entry with a topic.
    /// </summary>
    public interface ITopicAuditEntryData
    {        /// <summary>
        /// The name of the topic added to the repository
        /// </summary>
        Topic Topic { get; set; }

        /// <summary>
        /// The name of the topic added to the repository
        /// </summary>
        string TopicName { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    internal class TopicAuditEntryData : ITopicAuditEntryData
    {
        public Topic Topic { get; set; }

        public string TopicName { get; set; }
    }
}