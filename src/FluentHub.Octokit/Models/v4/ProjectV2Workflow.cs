namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A workflow inside a project.
    /// </summary>
    public class ProjectV2Workflow
    {
        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was created."
        /// <summary>
        public string CreatedAtHumanized { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; set; }

        /// <summary>
        /// The workflows' enabled state.
        /// </summary>
        public bool Enabled { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The workflows' name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The workflows' number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// The project that contains this workflow.
        /// </summary>
        public ProjectV2 Project { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was last updated."
        /// <summary>
        public string UpdatedAtHumanized { get; set; }
    }
}