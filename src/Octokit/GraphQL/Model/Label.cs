namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A label for categorizing Issues, Pull Requests, Milestones, or Discussions with a given Repository.
    /// </summary>
    public class Label : QueryableValue<Label>
    {
        internal Label(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the label color.
        /// </summary>
        public string Color { get; }

        /// <summary>
        /// Identifies the date and time when the label was created.
        /// </summary>
        public DateTimeOffset? CreatedAt { get; }

        /// <summary>
        /// A brief description of this label.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The Node ID of the Label object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Indicates whether or not this is a default label.
        /// </summary>
        public bool IsDefault { get; }

        /// <summary>
        /// A list of issues associated with this label.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="filterBy">Filtering options for issues returned from the connection.</param>
        /// <param name="labels">A list of label names to filter the pull requests by.</param>
        /// <param name="orderBy">Ordering options for issues returned from the connection.</param>
        /// <param name="states">A list of states to filter the issues by.</param>
        public IssueConnection Issues(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IssueFilters>? filterBy = null, Arg<IEnumerable<string>>? labels = null, Arg<IssueOrder>? orderBy = null, Arg<IEnumerable<IssueState>>? states = null) => this.CreateMethodCall(x => x.Issues(first, after, last, before, filterBy, labels, orderBy, states), Octokit.GraphQL.Model.IssueConnection.Create);

        /// <summary>
        /// Identifies the label name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A list of pull requests associated with this label.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="baseRefName">The base ref name to filter the pull requests by.</param>
        /// <param name="headRefName">The head ref name to filter the pull requests by.</param>
        /// <param name="labels">A list of label names to filter the pull requests by.</param>
        /// <param name="orderBy">Ordering options for pull requests returned from the connection.</param>
        /// <param name="states">A list of states to filter the pull requests by.</param>
        public PullRequestConnection PullRequests(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<string>? baseRefName = null, Arg<string>? headRefName = null, Arg<IEnumerable<string>>? labels = null, Arg<IssueOrder>? orderBy = null, Arg<IEnumerable<PullRequestState>>? states = null) => this.CreateMethodCall(x => x.PullRequests(first, after, last, before, baseRefName, headRefName, labels, orderBy, states), Octokit.GraphQL.Model.PullRequestConnection.Create);

        /// <summary>
        /// The repository associated with this label.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The HTTP path for this label.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Identifies the date and time when the label was last updated.
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this label.
        /// </summary>
        public string Url { get; }

        internal static Label Create(Expression expression)
        {
            return new Label(expression);
        }
    }
}