namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A repository-topic connects a repository to a topic.
    /// </summary>
    public class RepositoryTopic
    {
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