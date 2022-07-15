namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A character position in a check annotation.
    /// </summary>
    public class CheckAnnotationPosition
    {
        /// <summary>
        /// Column number (1 indexed).
        /// </summary>
        public int? Column { get; set; }

        /// <summary>
        /// Line number (1 indexed).
        /// </summary>
        public int Line { get; set; }
    }
}