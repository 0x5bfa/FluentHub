namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A list of results that matched against a search query. Regardless of the number of matches, a maximum of 1,000 results will be available across all types, potentially split across many pages.
    /// </summary>
    public class SearchResultItemConnection : QueryableValue<SearchResultItemConnection>, IPagingConnection<SearchResultItem>
    {
        internal SearchResultItemConnection(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The total number of pieces of code that matched the search query. Regardless of the total number of matches, a maximum of 1,000 results will be available across all types.
        /// </summary>
        public int CodeCount { get; }

        /// <summary>
        /// The total number of discussions that matched the search query. Regardless of the total number of matches, a maximum of 1,000 results will be available across all types.
        /// </summary>
        public int DiscussionCount { get; }

        /// <summary>
        /// A list of edges.
        /// </summary>
        public IQueryableList<SearchResultItemEdge> Edges => this.CreateProperty(x => x.Edges);

        /// <summary>
        /// The total number of issues that matched the search query. Regardless of the total number of matches, a maximum of 1,000 results will be available across all types.
        /// </summary>
        public int IssueCount { get; }

        /// <summary>
        /// A list of nodes.
        /// </summary>
        public IQueryableList<SearchResultItem> Nodes => this.CreateProperty(x => x.Nodes);

        /// <summary>
        /// Information to aid in pagination.
        /// </summary>
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Octokit.GraphQL.Model.PageInfo.Create);

        /// <summary>
        /// The total number of repositories that matched the search query. Regardless of the total number of matches, a maximum of 1,000 results will be available across all types.
        /// </summary>
        public int RepositoryCount { get; }

        /// <summary>
        /// The total number of users that matched the search query. Regardless of the total number of matches, a maximum of 1,000 results will be available across all types.
        /// </summary>
        public int UserCount { get; }

        /// <summary>
        /// The total number of wiki pages that matched the search query. Regardless of the total number of matches, a maximum of 1,000 results will be available across all types.
        /// </summary>
        public int WikiCount { get; }

        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static SearchResultItemConnection Create(Expression expression)
        {
            return new SearchResultItemConnection(expression);
        }
    }
}