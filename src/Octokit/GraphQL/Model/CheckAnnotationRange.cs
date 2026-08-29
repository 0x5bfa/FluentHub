namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information from a check run analysis to specific lines of code.
    /// </summary>
    public class CheckAnnotationRange
    {
        /// <summary>
        /// The starting line of the range.
        /// </summary>
        public int StartLine { get; set; }

        /// <summary>
        /// The starting column of the range.
        /// </summary>
        public int? StartColumn { get; set; }

        /// <summary>
        /// The ending line of the range.
        /// </summary>
        public int EndLine { get; set; }

        /// <summary>
        /// The ending column of the range.
        /// </summary>
        public int? EndColumn { get; set; }
    }
}