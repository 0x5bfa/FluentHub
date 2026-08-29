namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents either a issue the viewer can access or a restricted contribution.
    /// </summary>
    public class CreatedIssueOrRestrictedContribution : QueryableValue<CreatedIssueOrRestrictedContribution>, IUnion
    {
        internal CreatedIssueOrRestrictedContribution(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// Represents the contribution a user made on GitHub by opening an issue.
            /// </summary>
            public Selector<T> CreatedIssueContribution(Func<CreatedIssueContribution, T> selector) => default;

            /// <summary>
            /// Represents a private contribution a user made on GitHub.
            /// </summary>
            public Selector<T> RestrictedContribution(Func<RestrictedContribution, T> selector) => default;
        }

        internal static CreatedIssueOrRestrictedContribution Create(Expression expression)
        {
            return new CreatedIssueOrRestrictedContribution(expression);
        }
    }
}