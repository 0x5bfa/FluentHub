namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A check suite.
    /// </summary>
    public class CheckSuite : QueryableValue<CheckSuite>
    {
        internal CheckSuite(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The GitHub App which created this check suite.
        /// </summary>
        public App App => this.CreateProperty(x => x.App, Octokit.GraphQL.Model.App.Create);

        /// <summary>
        /// The name of the branch for this check suite.
        /// </summary>
        public Ref Branch => this.CreateProperty(x => x.Branch, Octokit.GraphQL.Model.Ref.Create);

        /// <summary>
        /// The check runs associated with a check suite.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="filterBy">Filters the check runs by this type.</param>
        public CheckRunConnection CheckRuns(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<CheckRunFilter>? filterBy = null) => this.CreateMethodCall(x => x.CheckRuns(first, after, last, before, filterBy), Octokit.GraphQL.Model.CheckRunConnection.Create);

        /// <summary>
        /// The commit for this check suite
        /// </summary>
        public Commit Commit => this.CreateProperty(x => x.Commit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// The conclusion of this check suite.
        /// </summary>
        public CheckConclusionState? Conclusion { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The user who triggered the check suite.
        /// </summary>
        public User Creator => this.CreateProperty(x => x.Creator, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The Node ID of the CheckSuite object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// A list of open pull requests matching the check suite.
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
        public PullRequestConnection MatchingPullRequests(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<string>? baseRefName = null, Arg<string>? headRefName = null, Arg<IEnumerable<string>>? labels = null, Arg<IssueOrder>? orderBy = null, Arg<IEnumerable<PullRequestState>>? states = null) => this.CreateMethodCall(x => x.MatchingPullRequests(first, after, last, before, baseRefName, headRefName, labels, orderBy, states), Octokit.GraphQL.Model.PullRequestConnection.Create);

        /// <summary>
        /// The push that triggered this check suite.
        /// </summary>
        public Push Push => this.CreateProperty(x => x.Push, Octokit.GraphQL.Model.Push.Create);

        /// <summary>
        /// The repository associated with this check suite.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The HTTP path for this check suite
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The status of this check suite.
        /// </summary>
        public CheckStatusState Status { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this check suite
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// The workflow run associated with this check suite.
        /// </summary>
        public WorkflowRun WorkflowRun => this.CreateProperty(x => x.WorkflowRun, Octokit.GraphQL.Model.WorkflowRun.Create);

        internal static CheckSuite Create(Expression expression)
        {
            return new CheckSuite(expression);
        }
    }
}