namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A goal associated with a GitHub Sponsors listing, representing a target the sponsored maintainer would like to attain.
    /// </summary>
    public class SponsorsGoal : QueryableValue<SponsorsGoal>
    {
        internal SponsorsGoal(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A description of the goal from the maintainer.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// What the objective of this goal is.
        /// </summary>
        public SponsorsGoalKind Kind { get; }

        /// <summary>
        /// The percentage representing how complete this goal is, between 0-100.
        /// </summary>
        public int PercentComplete { get; }

        /// <summary>
        /// What the goal amount is. Represents an amount in USD for monthly sponsorship amount goals. Represents a count of unique sponsors for total sponsors count goals.
        /// </summary>
        public int TargetValue { get; }

        /// <summary>
        /// A brief summary of the kind and target value of this goal.
        /// </summary>
        public string Title { get; }

        internal static SponsorsGoal Create(Expression expression)
        {
            return new SponsorsGoal(expression);
        }
    }
}