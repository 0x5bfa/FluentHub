namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Types that can grant permissions on a repository to a user
    /// </summary>
    public class PermissionGranter : QueryableValue<PermissionGranter>, IUnion
    {
        internal PermissionGranter(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// An account on GitHub, with one or more owners, that has repositories, members and teams.
            /// </summary>
            public Selector<T> Organization(Func<Organization, T> selector) => default;

            /// <summary>
            /// A repository contains the content for a project.
            /// </summary>
            public Selector<T> Repository(Func<Repository, T> selector) => default;

            /// <summary>
            /// A team of users in an organization.
            /// </summary>
            public Selector<T> Team(Func<Team, T> selector) => default;
        }

        internal static PermissionGranter Create(Expression expression)
        {
            return new PermissionGranter(expression);
        }
    }
}