namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An organization teams hovercard context
    /// </summary>
    public class OrganizationTeamsHovercardContext : QueryableValue<OrganizationTeamsHovercardContext>
    {
        internal OrganizationTeamsHovercardContext(Expression expression) : base(expression)
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
        /// Teams in this organization the user is a member of that are relevant
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public TeamConnection RelevantTeams(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.RelevantTeams(first, after, last, before), Octokit.GraphQL.Model.TeamConnection.Create);

        /// <summary>
        /// The path for the full team list for this user
        /// </summary>
        public string TeamsResourcePath { get; }

        /// <summary>
        /// The URL for the full team list for this user
        /// </summary>
        public string TeamsUrl { get; }

        /// <summary>
        /// The total number of teams the user is on in the organization
        /// </summary>
        public int TotalTeamCount { get; }

        internal static OrganizationTeamsHovercardContext Create(Expression expression)
        {
            return new OrganizationTeamsHovercardContext(expression);
        }
    }
}