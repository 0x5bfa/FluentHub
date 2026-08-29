namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents the rollup for both the check runs and status for a commit.
    /// </summary>
    public class StatusCheckRollup : QueryableValue<StatusCheckRollup>
    {
        internal StatusCheckRollup(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The commit the status and check runs are attached to.
        /// </summary>
        public Commit Commit => this.CreateProperty(x => x.Commit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// A list of status contexts and check runs for this commit.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public StatusCheckRollupContextConnection Contexts(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Contexts(first, after, last, before), Octokit.GraphQL.Model.StatusCheckRollupContextConnection.Create);

        /// <summary>
        /// The Node ID of the StatusCheckRollup object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The combined status for the commit.
        /// </summary>
        public StatusState State { get; }

        internal static StatusCheckRollup Create(Expression expression)
        {
            return new StatusCheckRollup(expression);
        }
    }
}