namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a Milestone object on a given repository.
    /// </summary>
    public class Milestone
    {
        /// <summary>
        /// `true` if the object is closed (definition of closed may depend on type)
        /// </summary>
        public bool Closed { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was closed.
        /// </summary>
        public DateTimeOffset? ClosedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was closed."
        /// <summary>
        public string ClosedAtHumanized { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was created."
        /// <summary>
        public string CreatedAtHumanized { get; set; }

        /// <summary>
        /// Identifies the actor who created the milestone.
        /// </summary>
        public IActor Creator { get; set; }

        /// <summary>
        /// Identifies the description of the milestone.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Identifies the due date of the milestone.
        /// </summary>
        public DateTimeOffset? DueOn { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the due date of the milestone."
        /// <summary>
        public string DueOnHumanized { get; set; }

        public ID Id { get; set; }

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
        public IssueConnection Issues { get; set; }

        /// <summary>
        /// Identifies the number of the milestone.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Identifies the percentage complete for the milestone
        /// </summary>
        public double ProgressPercentage { get; set; }

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
        public PullRequestConnection PullRequests { get; set; }

        /// <summary>
        /// The repository associated with this milestone.
        /// </summary>
        public Repository Repository { get; set; }

        /// <summary>
        /// The HTTP path for this milestone
        /// </summary>
        public string ResourcePath { get; set; }

        /// <summary>
        /// Identifies the state of the milestone.
        /// </summary>
        public MilestoneState State { get; set; }

        /// <summary>
        /// Identifies the title of the milestone.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was last updated."
        /// <summary>
        public string UpdatedAtHumanized { get; set; }

        /// <summary>
        /// The HTTP URL for this milestone
        /// </summary>
        public string Url { get; set; }
    }
}