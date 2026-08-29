namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class PullRequestChangedFileEdge : QueryableValue<PullRequestChangedFileEdge>
    {
        internal PullRequestChangedFileEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public PullRequestChangedFile Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.PullRequestChangedFile.Create);

        internal static PullRequestChangedFileEdge Create(Expression expression)
        {
            return new PullRequestChangedFileEdge(expression);
        }
    }
}