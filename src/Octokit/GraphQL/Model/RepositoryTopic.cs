namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A repository-topic connects a repository to a topic.
    /// </summary>
    public class RepositoryTopic : QueryableValue<RepositoryTopic>
    {
        internal RepositoryTopic(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The Node ID of the RepositoryTopic object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The HTTP path for this repository-topic.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The topic.
        /// </summary>
        public Topic Topic => this.CreateProperty(x => x.Topic, Octokit.GraphQL.Model.Topic.Create);

        /// <summary>
        /// The HTTP URL for this repository-topic.
        /// </summary>
        public string Url { get; }

        internal static RepositoryTopic Create(Expression expression)
        {
            return new RepositoryTopic(expression);
        }
    }
}