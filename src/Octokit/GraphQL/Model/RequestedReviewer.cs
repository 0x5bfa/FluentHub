namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Types that can be requested reviewers.
    /// </summary>
    public class RequestedReviewer : QueryableValue<RequestedReviewer>, IUnion
    {
        internal RequestedReviewer(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// A special type of user which takes actions on behalf of GitHub Apps.
            /// </summary>
            public Selector<T> Bot(Func<Bot, T> selector) => default;

            /// <summary>
            /// A placeholder user for attribution of imported data on GitHub.
            /// </summary>
            public Selector<T> Mannequin(Func<Mannequin, T> selector) => default;

            /// <summary>
            /// A team of users in an organization.
            /// </summary>
            public Selector<T> Team(Func<Team, T> selector) => default;

            /// <summary>
            /// A user is an individual's account on GitHub that owns repositories and can make new content.
            /// </summary>
            public Selector<T> User(Func<User, T> selector) => default;
        }

        internal static RequestedReviewer Create(Expression expression)
        {
            return new RequestedReviewer(expression);
        }
    }
}