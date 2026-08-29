namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Descriptive details about the check run.
    /// </summary>
    public class CheckRunOutput
    {
        /// <summary>
        /// A title to provide for this check run.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The summary of the check run (supports Commonmark).
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// The details of the check run (supports Commonmark).
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The annotations that are made as part of the check run.
        /// </summary>
        public IEnumerable<CheckAnnotationData> Annotations { get; set; }

        /// <summary>
        /// Images attached to the check run output displayed in the GitHub pull request UI.
        /// </summary>
        public IEnumerable<CheckRunOutputImage> Images { get; set; }
    }
}