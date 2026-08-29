namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Types that can be added to a user list.
    /// </summary>
    public class UserListItems : QueryableValue<UserListItems>, IUnion
    {
        internal UserListItems(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// A repository contains the content for a project.
            /// </summary>
            public Selector<T> Repository(Func<Repository, T> selector) => default;
        }

        internal static UserListItems Create(Expression expression)
        {
            return new UserListItems(expression);
        }
    }
}