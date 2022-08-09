namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Configurations for project fields.
    /// </summary>
    public class ProjectV2FieldConfiguration
    {
        /// <summary>
        /// A field inside a project.
        /// </summary>
        public ProjectV2Field ProjectV2Field { get; set; }

        /// <summary>
        /// An iteration field inside a project.
        /// </summary>
        public ProjectV2IterationField ProjectV2IterationField { get; set; }

        /// <summary>
        /// A single select field inside a project.
        /// </summary>
        public ProjectV2SingleSelectField ProjectV2SingleSelectField { get; set; }
    }
}