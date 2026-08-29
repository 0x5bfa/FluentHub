namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Used for argument of CreateProjectV2 mutation.
    /// </summary>
    public class OrganizationOrUser : QueryableValue<OrganizationOrUser>, IUnion
    {
        internal OrganizationOrUser(Expression expression) : base(expression)
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
            /// A user is an individual's account on GitHub that owns repositories and can make new content.
            /// </summary>
            public Selector<T> User(Func<User, T> selector) => default;
        }

        internal static OrganizationOrUser Create(Expression expression)
        {
            return new OrganizationOrUser(expression);
        }
    }
}