namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// This aggregates commits made by a user within one repository.
    /// </summary>
    public class CommitContributionsByRepository : QueryableValue<CommitContributionsByRepository>
    {
        internal CommitContributionsByRepository(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The commit contributions, each representing a day.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for commit contributions returned from the connection.</param>
        public CreatedCommitContributionConnection Contributions(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<CommitContributionOrder>? orderBy = null) => this.CreateMethodCall(x => x.Contributions(first, after, last, before, orderBy), Octokit.GraphQL.Model.CreatedCommitContributionConnection.Create);

        /// <summary>
        /// The repository in which the commits were made.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The HTTP path for the user's commits to the repository in this time range.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The HTTP URL for the user's commits to the repository in this time range.
        /// </summary>
        public string Url { get; }

        internal static CommitContributionsByRepository Create(Expression expression)
        {
            return new CommitContributionsByRepository(expression);
        }
    }
}