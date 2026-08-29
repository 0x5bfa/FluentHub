namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The results of a search.
    /// </summary>
    public class SearchResultItem : QueryableValue<SearchResultItem>, IUnion
    {
        internal SearchResultItem(Expression expression) : base(expression)
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
            /// A discussion in a repository.
            /// </summary>
            public Selector<T> Discussion(Func<Discussion, T> selector) => default;

            /// <summary>
            /// An Issue is a place to discuss ideas, enhancements, tasks, and bugs for a project.
            /// </summary>
            public Selector<T> Issue(Func<Issue, T> selector) => default;

            /// <summary>
            /// A listing in the GitHub integration marketplace.
            /// </summary>
            public Selector<T> MarketplaceListing(Func<MarketplaceListing, T> selector) => default;

            /// <summary>
            /// An account on GitHub, with one or more owners, that has repositories, members and teams.
            /// </summary>
            public Selector<T> Organization(Func<Organization, T> selector) => default;

            /// <summary>
            /// A repository pull request.
            /// </summary>
            public Selector<T> PullRequest(Func<PullRequest, T> selector) => default;

            /// <summary>
            /// A repository contains the content for a project.
            /// </summary>
            public Selector<T> Repository(Func<Repository, T> selector) => default;

            /// <summary>
            /// A user is an individual's account on GitHub that owns repositories and can make new content.
            /// </summary>
            public Selector<T> User(Func<User, T> selector) => default;
        }

        internal static SearchResultItem Create(Expression expression)
        {
            return new SearchResultItem(expression);
        }
    }
}