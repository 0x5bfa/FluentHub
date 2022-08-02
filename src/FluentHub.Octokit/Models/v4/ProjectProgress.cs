namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Project progress stats.
    /// </summary>
    public class ProjectProgress
    {
        /// <summary>
        /// The number of done cards.
        /// </summary>
        public int DoneCount { get; set; }

        /// <summary>
        /// The percentage of done cards.
        /// </summary>
        public double DonePercentage { get; set; }

        /// <summary>
        /// Whether progress tracking is enabled and cards with purpose exist for this project
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// The number of in-progress cards.
        /// </summary>
        public int InProgressCount { get; set; }

        /// <summary>
        /// The percentage of in-progress cards.
        /// </summary>
        public double InProgressPercentage { get; set; }

        /// <summary>
        /// The number of to do cards.
        /// </summary>
        public int TodoCount { get; set; }

        /// <summary>
        /// The percentage of to do cards.
        /// </summary>
        public double TodoPercentage { get; set; }
    }
}