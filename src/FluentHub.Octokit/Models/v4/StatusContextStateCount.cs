namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a count of the state of a status context.
    /// </summary>
    public class StatusContextStateCount
    {
        /// <summary>
        /// The number of statuses with this state.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// The state of a status context.
        /// </summary>
        public StatusState State { get; set; }
    }
}