namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Types that can represent a repository ruleset bypass actor.
    /// </summary>
    public class BypassActor : QueryableValue<BypassActor>, IUnion
    {
        internal BypassActor(Expression expression) : base(expression)
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
            /// A team of users in an organization.
            /// </summary>
            public Selector<T> Team(Func<Team, T> selector) => default;
        }

        internal static BypassActor Create(Expression expression)
        {
            return new BypassActor(expression);
        }
    }
}