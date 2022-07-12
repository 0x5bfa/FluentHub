namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An inclusive pair of positions for a check annotation.
    /// </summary>
    public class CheckAnnotationSpan
    {
        /// <summary>
        /// End position (inclusive).
        /// </summary>
        public CheckAnnotationPosition End { get; set; }

        /// <summary>
        /// Start position (inclusive).
        /// </summary>
        public CheckAnnotationPosition Start { get; set; }
    }
}