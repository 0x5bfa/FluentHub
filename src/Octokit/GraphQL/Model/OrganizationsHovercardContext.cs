namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An organization list hovercard context
    /// </summary>
    public class OrganizationsHovercardContext : QueryableValue<OrganizationsHovercardContext>
    {
        internal OrganizationsHovercardContext(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A string describing this context
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// An octicon to accompany this context
        /// </summary>
        public string Octicon { get; }

        /// <summary>
        /// Organizations this user is a member of that are relevant
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for the User's organizations.</param>
        public OrganizationConnection RelevantOrganizations(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrganizationOrder>? orderBy = null) => this.CreateMethodCall(x => x.RelevantOrganizations(first, after, last, before, orderBy), Octokit.GraphQL.Model.OrganizationConnection.Create);

        /// <summary>
        /// The total number of organizations this user is in
        /// </summary>
        public int TotalOrganizationCount { get; }

        internal static OrganizationsHovercardContext Create(Expression expression)
        {
            return new OrganizationsHovercardContext(expression);
        }
    }
}