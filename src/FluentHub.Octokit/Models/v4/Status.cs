namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a commit status.
    /// </summary>
    public class Status
    {
        /// <summary>
        /// A list of status contexts and check runs for this commit.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public StatusCheckRollupContextConnection CombinedContexts { get; set; }

        /// <summary>
        /// The commit this status is attached to.
        /// </summary>
        public Commit Commit { get; set; }

        /// <summary>
        /// Looks up an individual status context by context name.
        /// </summary>
        /// <param name="name">The context name.</param>
        public StatusContext Context { get; set; }

        /// <summary>
        /// The individual status contexts for this commit.
        /// </summary>
        public List<StatusContext> Contexts { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The combined commit status.
        /// </summary>
        public StatusState State { get; set; }
    }
}