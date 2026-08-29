namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Project field values
    /// </summary>
    public class ProjectV2ItemFieldValue : QueryableValue<ProjectV2ItemFieldValue>, IUnion
    {
        internal ProjectV2ItemFieldValue(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// The value of a date field in a Project item.
            /// </summary>
            public Selector<T> ProjectV2ItemFieldDateValue(Func<ProjectV2ItemFieldDateValue, T> selector) => default;

            /// <summary>
            /// The value of an iteration field in a Project item.
            /// </summary>
            public Selector<T> ProjectV2ItemFieldIterationValue(Func<ProjectV2ItemFieldIterationValue, T> selector) => default;

            /// <summary>
            /// The value of the labels field in a Project item.
            /// </summary>
            public Selector<T> ProjectV2ItemFieldLabelValue(Func<ProjectV2ItemFieldLabelValue, T> selector) => default;

            /// <summary>
            /// The value of a milestone field in a Project item.
            /// </summary>
            public Selector<T> ProjectV2ItemFieldMilestoneValue(Func<ProjectV2ItemFieldMilestoneValue, T> selector) => default;

            /// <summary>
            /// The value of a number field in a Project item.
            /// </summary>
            public Selector<T> ProjectV2ItemFieldNumberValue(Func<ProjectV2ItemFieldNumberValue, T> selector) => default;

            /// <summary>
            /// The value of a pull request field in a Project item.
            /// </summary>
            public Selector<T> ProjectV2ItemFieldPullRequestValue(Func<ProjectV2ItemFieldPullRequestValue, T> selector) => default;

            /// <summary>
            /// The value of a repository field in a Project item.
            /// </summary>
            public Selector<T> ProjectV2ItemFieldRepositoryValue(Func<ProjectV2ItemFieldRepositoryValue, T> selector) => default;

            /// <summary>
            /// The value of a reviewers field in a Project item.
            /// </summary>
            public Selector<T> ProjectV2ItemFieldReviewerValue(Func<ProjectV2ItemFieldReviewerValue, T> selector) => default;

            /// <summary>
            /// The value of a single select field in a Project item.
            /// </summary>
            public Selector<T> ProjectV2ItemFieldSingleSelectValue(Func<ProjectV2ItemFieldSingleSelectValue, T> selector) => default;

            /// <summary>
            /// The value of a text field in a Project item.
            /// </summary>
            public Selector<T> ProjectV2ItemFieldTextValue(Func<ProjectV2ItemFieldTextValue, T> selector) => default;

            /// <summary>
            /// The value of a user field in a Project item.
            /// </summary>
            public Selector<T> ProjectV2ItemFieldUserValue(Func<ProjectV2ItemFieldUserValue, T> selector) => default;
        }

        internal static ProjectV2ItemFieldValue Create(Expression expression)
        {
            return new ProjectV2ItemFieldValue(expression);
        }
    }
}