namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// The value of an iteration field in a Project item.
    /// </summary>
    public class ProjectV2ItemFieldIterationValue
    {
        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The actor who created the item.
        /// </summary>
        public IActor Creator { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; set; }

        /// <summary>
        /// The duration of the iteration in days.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// The project field that contains this value.
        /// </summary>
        public ProjectV2FieldConfiguration Field { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The project item that contains this value.
        /// </summary>
        public ProjectV2Item Item { get; set; }

        /// <summary>
        /// The ID of the iteration.
        /// </summary>
        public string IterationId { get; set; }

        /// <summary>
        /// The start date of the iteration.
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// The title of the iteration.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The title of the iteration, with HTML.
        /// </summary>
        public string TitleHTML { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }
    }
}