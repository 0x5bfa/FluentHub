namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a count of the state of a check run.
    /// </summary>
    public class CheckRunStateCount
    {
        /// <summary>
        /// The number of check runs with this state.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// The state of a check run.
        /// </summary>
        public CheckRunState State { get; set; }
    }
}