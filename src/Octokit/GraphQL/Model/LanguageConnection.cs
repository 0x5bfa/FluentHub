namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A list of languages associated with the parent.
    /// </summary>
    public class LanguageConnection : QueryableValue<LanguageConnection>, IPagingConnection<Language>
    {
        internal LanguageConnection(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of edges.
        /// </summary>
        public IQueryableList<LanguageEdge> Edges => this.CreateProperty(x => x.Edges);

        /// <summary>
        /// A list of nodes.
        /// </summary>
        public IQueryableList<Language> Nodes => this.CreateProperty(x => x.Nodes);

        /// <summary>
        /// Information to aid in pagination.
        /// </summary>
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Octokit.GraphQL.Model.PageInfo.Create);

        /// <summary>
        /// Identifies the total count of items in the connection.
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// The total size in bytes of files written in that language.
        /// </summary>
        public int TotalSize { get; }

        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static LanguageConnection Create(Expression expression)
        {
            return new LanguageConnection(expression);
        }
    }
}