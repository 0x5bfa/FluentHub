namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An item within a Project.
    /// </summary>
    public class ProjectV2Item
    {
        /// <summary>
        /// The content of the referenced draft issue, issue, or pull request
        /// </summary>
        public ProjectV2ItemContent Content { get; set; }

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
        /// List of field values
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ProjectV2ItemFieldValueConnection FieldValues { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Whether the item is archived.
        /// </summary>
        public bool IsArchived { get; set; }

        /// <summary>
        /// The project that contains this item.
        /// </summary>
        public ProjectV2 Project { get; set; }

        /// <summary>
        /// The type of the item.
        /// </summary>
        public ProjectV2ItemType Type { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }
    }
}