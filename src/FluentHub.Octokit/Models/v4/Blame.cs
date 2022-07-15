namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a Git blame.
    /// </summary>
    public class Blame
    {
        /// <summary>
        /// The list of ranges from a Git blame.
        /// </summary>
        public List<BlameRange> Ranges { get; set; }
    }
}