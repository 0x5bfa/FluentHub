namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Entities that can sponsor others via GitHub Sponsors
    /// </summary>
    public class Sponsor : QueryableValue<Sponsor>, IUnion
    {
        internal Sponsor(Expression expression) : base(expression)
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

        internal static Sponsor Create(Expression expression)
        {
            return new Sponsor(expression);
        }
    }
}