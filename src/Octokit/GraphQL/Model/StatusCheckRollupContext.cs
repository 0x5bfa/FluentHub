namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Types that can be inside a StatusCheckRollup context.
    /// </summary>
    public class StatusCheckRollupContext : QueryableValue<StatusCheckRollupContext>, IUnion
    {
        internal StatusCheckRollupContext(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// A check run.
            /// </summary>
            public Selector<T> CheckRun(Func<CheckRun, T> selector) => default;

            /// <summary>
            /// Represents an individual commit status context
            /// </summary>
            public Selector<T> StatusContext(Func<StatusContext, T> selector) => default;
        }

        internal static StatusCheckRollupContext Create(Expression expression)
        {
            return new StatusCheckRollupContext(expression);
        }
    }
}