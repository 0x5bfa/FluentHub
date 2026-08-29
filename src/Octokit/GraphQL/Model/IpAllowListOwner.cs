namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Types that can own an IP allow list.
    /// </summary>
    public class IpAllowListOwner : QueryableValue<IpAllowListOwner>, IUnion
    {
        internal IpAllowListOwner(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// A GitHub App.
            /// </summary>
            public Selector<T> App(Func<App, T> selector) => default;

            /// <summary>
            /// An account to manage multiple organizations with consolidated policy and billing.
            /// </summary>
            public Selector<T> Enterprise(Func<Enterprise, T> selector) => default;

            /// <summary>
            /// An account on GitHub, with one or more owners, that has repositories, members and teams.
            /// </summary>
            public Selector<T> Organization(Func<Organization, T> selector) => default;
        }

        internal static IpAllowListOwner Create(Expression expression)
        {
            return new IpAllowListOwner(expression);
        }
    }
}