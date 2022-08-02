namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents the rollup for both the check runs and status for a commit.
    /// </summary>
    public class StatusCheckRollup
    {
        /// <summary>
        /// The commit the status and check runs are attached to.
        /// </summary>
        public Commit Commit { get; set; }

        /// <summary>
        /// A list of status contexts and check runs for this commit.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public StatusCheckRollupContextConnection Contexts { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The combined status for the commit.
        /// </summary>
        public StatusState State { get; set; }
    }
}