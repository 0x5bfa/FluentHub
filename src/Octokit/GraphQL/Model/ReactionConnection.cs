namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A list of reactions that have been left on the subject.
    /// </summary>
    public class ReactionConnection : QueryableValue<ReactionConnection>, IPagingConnection<Reaction>
    {
        internal ReactionConnection(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of edges.
        /// </summary>
        public IQueryableList<ReactionEdge> Edges => this.CreateProperty(x => x.Edges);

        /// <summary>
        /// A list of nodes.
        /// </summary>
        public IQueryableList<Reaction> Nodes => this.CreateProperty(x => x.Nodes);

        /// <summary>
        /// Information to aid in pagination.
        /// </summary>
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Octokit.GraphQL.Model.PageInfo.Create);

        /// <summary>
        /// Identifies the total count of items in the connection.
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// Whether or not the authenticated user has left a reaction on the subject.
        /// </summary>
        public bool ViewerHasReacted { get; }

        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static ReactionConnection Create(Expression expression)
        {
            return new ReactionConnection(expression);
        }
    }
}