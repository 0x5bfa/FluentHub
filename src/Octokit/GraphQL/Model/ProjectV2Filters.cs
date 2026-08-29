namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ways in which to filter lists of projects.
    /// </summary>
    public class ProjectV2Filters
    {
        /// <summary>
        /// List project v2 filtered by the state given.
        /// </summary>
        public ProjectV2State? State { get; set; }
    }
}