namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Types which can be parameters for `RepositoryRule` objects.
    /// </summary>
    public class RuleParameters : QueryableValue<RuleParameters>, IUnion
    {
        internal RuleParameters(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// Parameters to be used for the branch_name_pattern rule
            /// </summary>
            public Selector<T> BranchNamePatternParameters(Func<BranchNamePatternParameters, T> selector) => default;

            /// <summary>
            /// Parameters to be used for the commit_author_email_pattern rule
            /// </summary>
            public Selector<T> CommitAuthorEmailPatternParameters(Func<CommitAuthorEmailPatternParameters, T> selector) => default;

            /// <summary>
            /// Parameters to be used for the commit_message_pattern rule
            /// </summary>
            public Selector<T> CommitMessagePatternParameters(Func<CommitMessagePatternParameters, T> selector) => default;

            /// <summary>
            /// Parameters to be used for the committer_email_pattern rule
            /// </summary>
            public Selector<T> CommitterEmailPatternParameters(Func<CommitterEmailPatternParameters, T> selector) => default;

            /// <summary>
            /// Require all commits be made to a non-target branch and submitted via a pull request before they can be merged.
            /// </summary>
            public Selector<T> PullRequestParameters(Func<PullRequestParameters, T> selector) => default;

            /// <summary>
            /// Choose which environments must be successfully deployed to before refs can be pushed into a ref that matches this rule.
            /// </summary>
            public Selector<T> RequiredDeploymentsParameters(Func<RequiredDeploymentsParameters, T> selector) => default;

            /// <summary>
            /// Choose which status checks must pass before the ref is updated. When enabled, commits must first be pushed to another ref where the checks pass.
            /// </summary>
            public Selector<T> RequiredStatusChecksParameters(Func<RequiredStatusChecksParameters, T> selector) => default;

            /// <summary>
            /// Parameters to be used for the tag_name_pattern rule
            /// </summary>
            public Selector<T> TagNamePatternParameters(Func<TagNamePatternParameters, T> selector) => default;

            /// <summary>
            /// Only allow users with bypass permission to update matching refs.
            /// </summary>
            public Selector<T> UpdateParameters(Func<UpdateParameters, T> selector) => default;

            /// <summary>
            /// Require all changes made to a targeted branch to pass the specified workflows before they can be merged.
            /// </summary>
            public Selector<T> WorkflowsParameters(Func<WorkflowsParameters, T> selector) => default;
        }

        internal static RuleParameters Create(Expression expression)
        {
            return new RuleParameters(expression);
        }
    }
}