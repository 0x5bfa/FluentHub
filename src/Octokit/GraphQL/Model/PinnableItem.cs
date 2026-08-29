namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Types that can be pinned to a profile page.
    /// </summary>
    public class PinnableItem : QueryableValue<PinnableItem>, IUnion
    {
        internal PinnableItem(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// A Gist.
            /// </summary>
            public Selector<T> Gist(Func<Gist, T> selector) => default;

            /// <summary>
            /// A repository contains the content for a project.
            /// </summary>
            public Selector<T> Repository(Func<Repository, T> selector) => default;
        }

        internal static PinnableItem Create(Expression expression)
        {
            return new PinnableItem(expression);
        }
    }
}