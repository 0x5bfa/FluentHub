namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A single check step.
    /// </summary>
    public class CheckStep : QueryableValue<CheckStep>
    {
        internal CheckStep(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the check step was completed.
        /// </summary>
        public DateTimeOffset? CompletedAt { get; }

        /// <summary>
        /// The conclusion of the check step.
        /// </summary>
        public CheckConclusionState? Conclusion { get; }

        /// <summary>
        /// A reference for the check step on the integrator's system.
        /// </summary>
        public string ExternalId { get; }

        /// <summary>
        /// The step's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The index of the step in the list of steps of the parent check run.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// Number of seconds to completion.
        /// </summary>
        public int? SecondsToCompletion { get; }

        /// <summary>
        /// Identifies the date and time when the check step was started.
        /// </summary>
        public DateTimeOffset? StartedAt { get; }

        /// <summary>
        /// The current status of the check step.
        /// </summary>
        public CheckStatusState Status { get; }

        internal static CheckStep Create(Expression expression)
        {
            return new CheckStep(expression);
        }
    }
}