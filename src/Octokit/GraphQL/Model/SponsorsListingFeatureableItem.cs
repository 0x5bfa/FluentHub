namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A record that can be featured on a GitHub Sponsors profile.
    /// </summary>
    public class SponsorsListingFeatureableItem : QueryableValue<SponsorsListingFeatureableItem>, IUnion
    {
        internal SponsorsListingFeatureableItem(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// A repository contains the content for a project.
            /// </summary>
            public Selector<T> Repository(Func<Repository, T> selector) => default;

            /// <summary>
            /// A user is an individual's account on GitHub that owns repositories and can make new content.
            /// </summary>
            public Selector<T> User(Func<User, T> selector) => default;
        }

        internal static SponsorsListingFeatureableItem Create(Expression expression)
        {
            return new SponsorsListingFeatureableItem(expression);
        }
    }
}