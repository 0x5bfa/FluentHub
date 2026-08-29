namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents either a pull request the viewer can access or a restricted contribution.
    /// </summary>
    public class CreatedPullRequestOrRestrictedContribution : QueryableValue<CreatedPullRequestOrRestrictedContribution>, IUnion
    {
        internal CreatedPullRequestOrRestrictedContribution(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// Represents the contribution a user made on GitHub by opening a pull request.
            /// </summary>
            public Selector<T> CreatedPullRequestContribution(Func<CreatedPullRequestContribution, T> selector) => default;

            /// <summary>
            /// Represents a private contribution a user made on GitHub.
            /// </summary>
            public Selector<T> RestrictedContribution(Func<RestrictedContribution, T> selector) => default;
        }

        internal static CreatedPullRequestOrRestrictedContribution Create(Expression expression)
        {
            return new CreatedPullRequestOrRestrictedContribution(expression);
        }
    }
}