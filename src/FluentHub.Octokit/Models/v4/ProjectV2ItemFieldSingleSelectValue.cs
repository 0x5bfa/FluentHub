namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// The value of a single select field in a Project item.
    /// </summary>
    public class ProjectV2ItemFieldSingleSelectValue
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
        /// The actor who created the item.
        /// </summary>
        public IActor Creator { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; set; }

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
        /// The name of the selected single select option.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The html name of the selected single select option.
        /// </summary>
        public string NameHTML { get; set; }

        /// <summary>
        /// The id of the selected single select option.
        /// </summary>
        public string OptionId { get; set; }

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