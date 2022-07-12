namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Iteration field configuration for a project.
    /// </summary>
    public class ProjectV2IterationFieldConfiguration
    {
        /// <summary>
        /// The iteration's completed iterations
        /// </summary>
        public List<ProjectV2IterationFieldIteration> CompletedIterations { get; set; }

        /// <summary>
        /// The iteration's duration in days
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// The iteration's iterations
        /// </summary>
        public List<ProjectV2IterationFieldIteration> Iterations { get; set; }

        /// <summary>
        /// The iteration's start day of the week
        /// </summary>
        public int StartDay { get; set; }
    }
}