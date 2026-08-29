namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Types that can be inside Project Items.
    /// </summary>
    public class ProjectV2ItemContent : QueryableValue<ProjectV2ItemContent>, IUnion
    {
        internal ProjectV2ItemContent(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// A draft issue within a project.
            /// </summary>
            public Selector<T> DraftIssue(Func<DraftIssue, T> selector) => default;

            /// <summary>
            /// An Issue is a place to discuss ideas, enhancements, tasks, and bugs for a project.
            /// </summary>
            public Selector<T> Issue(Func<Issue, T> selector) => default;

            /// <summary>
            /// A repository pull request.
            /// </summary>
            public Selector<T> PullRequest(Func<PullRequest, T> selector) => default;
        }

        internal static ProjectV2ItemContent Create(Expression expression)
        {
            return new ProjectV2ItemContent(expression);
        }
    }
}