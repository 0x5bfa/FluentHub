namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Information about pagination in a connection.
    /// </summary>
    public class PageInfo : QueryableValue<PageInfo>, IPageInfo
    {
        internal PageInfo(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// When paginating forwards, the cursor to continue.
        /// </summary>
        public string EndCursor { get; }

        /// <summary>
        /// When paginating forwards, are there more items?
        /// </summary>
        public bool HasNextPage { get; }

        /// <summary>
        /// When paginating backwards, are there more items?
        /// </summary>
        public bool HasPreviousPage { get; }

        /// <summary>
        /// When paginating backwards, the cursor to continue.
        /// </summary>
        public string StartCursor { get; }

        internal static PageInfo Create(Expression expression)
        {
            return new PageInfo(expression);
        }
    }
}