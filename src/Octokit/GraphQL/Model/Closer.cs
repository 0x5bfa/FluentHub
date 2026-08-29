namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The object which triggered a `ClosedEvent`.
    /// </summary>
    public class Closer : QueryableValue<Closer>, IUnion
    {
        internal Closer(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// Represents a Git commit.
            /// </summary>
            public Selector<T> Commit(Func<Commit, T> selector) => default;

            /// <summary>
            /// A repository pull request.
            /// </summary>
            public Selector<T> PullRequest(Func<PullRequest, T> selector) => default;
        }

        internal static Closer Create(Expression expression)
        {
            return new Closer(expression);
        }
    }
}