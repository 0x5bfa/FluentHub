namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The value of a pull request field in a Project item.
    /// </summary>
    public class ProjectV2ItemFieldPullRequestValue : QueryableValue<ProjectV2ItemFieldPullRequestValue>
    {
        internal ProjectV2ItemFieldPullRequestValue(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The field that contains this value.
        /// </summary>
        public ProjectV2FieldConfiguration Field => this.CreateProperty(x => x.Field, Octokit.GraphQL.Model.ProjectV2FieldConfiguration.Create);

        /// <summary>
        /// The pull requests for this field
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for pull requests.</param>
        public PullRequestConnection PullRequests(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<PullRequestOrder>? orderBy = null) => this.CreateMethodCall(x => x.PullRequests(first, after, last, before, orderBy), Octokit.GraphQL.Model.PullRequestConnection.Create);

        internal static ProjectV2ItemFieldPullRequestValue Create(Expression expression)
        {
            return new ProjectV2ItemFieldPullRequestValue(expression);
        }
    }
}