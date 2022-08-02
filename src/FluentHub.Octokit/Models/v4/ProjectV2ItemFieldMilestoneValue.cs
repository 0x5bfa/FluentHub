namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// The value of a milestone field in a Project item.
    /// </summary>
    public class ProjectV2ItemFieldMilestoneValue
    {
        /// <summary>
        /// The field that contains this value.
        /// </summary>
        public ProjectV2FieldConfiguration Field { get; set; }

        /// <summary>
        /// Milestone value of a field
        /// </summary>
        public Milestone Milestone { get; set; }
    }
}