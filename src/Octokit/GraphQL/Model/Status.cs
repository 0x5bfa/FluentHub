namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a commit status.
    /// </summary>
    public class Status : QueryableValue<Status>
    {
        internal Status(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of status contexts and check runs for this commit.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public StatusCheckRollupContextConnection CombinedContexts(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.CombinedContexts(first, after, last, before), Octokit.GraphQL.Model.StatusCheckRollupContextConnection.Create);

        /// <summary>
        /// The commit this status is attached to.
        /// </summary>
        public Commit Commit => this.CreateProperty(x => x.Commit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// Looks up an individual status context by context name.
        /// </summary>
        /// <param name="name">The context name.</param>
        public StatusContext Context(Arg<string> name) => this.CreateMethodCall(x => x.Context(name), Octokit.GraphQL.Model.StatusContext.Create);

        /// <summary>
        /// The individual status contexts for this commit.
        /// </summary>
        public IQueryableList<StatusContext> Contexts => this.CreateProperty(x => x.Contexts);

        /// <summary>
        /// The Node ID of the Status object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The combined commit status.
        /// </summary>
        public StatusState State { get; }

        internal static Status Create(Expression expression)
        {
            return new Status(expression);
        }
    }
}