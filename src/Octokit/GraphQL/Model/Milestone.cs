namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a Milestone object on a given repository.
    /// </summary>
    public class Milestone : QueryableValue<Milestone>
    {
        internal Milestone(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Indicates if the object is closed (definition of closed may depend on type)
        /// </summary>
        public bool Closed { get; }

        /// <summary>
        /// Identifies the date and time when the object was closed.
        /// </summary>
        public DateTimeOffset? ClosedAt { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the actor who created the milestone.
        /// </summary>
        public IActor Creator => this.CreateProperty(x => x.Creator, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the description of the milestone.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Identifies the due date of the milestone.
        /// </summary>
        public DateTimeOffset? DueOn { get; }

        /// <summary>
        /// The Node ID of the Milestone object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// A list of issues associated with the milestone.
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
        /// Identifies the number of the milestone.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// Identifies the percentage complete for the milestone
        /// </summary>
        public double ProgressPercentage { get; }

        /// <summary>
        /// A list of pull requests associated with the milestone.
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
        /// The repository associated with this milestone.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The HTTP path for this milestone
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Identifies the state of the milestone.
        /// </summary>
        public MilestoneState State { get; }

        /// <summary>
        /// Identifies the title of the milestone.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this milestone
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Indicates if the object can be closed by the viewer.
        /// </summary>
        public bool ViewerCanClose { get; }

        /// <summary>
        /// Indicates if the object can be reopened by the viewer.
        /// </summary>
        public bool ViewerCanReopen { get; }

        internal static Milestone Create(Expression expression)
        {
            return new Milestone(expression);
        }
    }
}