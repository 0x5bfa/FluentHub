namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// This aggregates issues opened by a user within one repository.
    /// </summary>
    public class IssueContributionsByRepository : QueryableValue<IssueContributionsByRepository>
    {
        internal IssueContributionsByRepository(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The issue contributions.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for contributions returned from the connection.</param>
        public CreatedIssueContributionConnection Contributions(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ContributionOrder>? orderBy = null) => this.CreateMethodCall(x => x.Contributions(first, after, last, before, orderBy), Octokit.GraphQL.Model.CreatedIssueContributionConnection.Create);

        /// <summary>
        /// The repository in which the issues were opened.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static IssueContributionsByRepository Create(Expression expression)
        {
            return new IssueContributionsByRepository(expression);
        }
    }
}