namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A column inside a project.
    /// </summary>
    public class ProjectColumn
    {
        /// <summary>
        /// List of cards in the column
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="archivedStates">A list of archived states to filter the cards by</param>
        public ProjectCardConnection Cards { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The project column's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The project that contains this column.
        /// </summary>
        public Project Project { get; set; }

        /// <summary>
        /// The semantic purpose of the column
        /// </summary>
        public ProjectColumnPurpose? Purpose { get; set; }

        /// <summary>
        /// The HTTP path for this project column
        /// </summary>
        public string ResourcePath { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// The HTTP URL for this project column
        /// </summary>
        public string Url { get; set; }
    }
}