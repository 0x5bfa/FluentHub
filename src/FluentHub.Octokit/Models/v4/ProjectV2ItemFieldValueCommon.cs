namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Common fields across different project field value types
    /// </summary>
    public interface IProjectV2ItemFieldValueCommon
    {        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The actor who created the item.
        /// </summary>
        IActor Creator { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        int? DatabaseId { get; set; }

        /// <summary>
        /// The project field that contains this value.
        /// </summary>
        ProjectV2FieldConfiguration Field { get; set; }

        ID Id { get; set; }

        /// <summary>
        /// The project item that contains this value.
        /// </summary>
        ProjectV2Item Item { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        DateTimeOffset UpdatedAt { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class ProjectV2ItemFieldValueCommon : IProjectV2ItemFieldValueCommon
    {
        public DateTimeOffset CreatedAt { get; set; }

        public IActor Creator { get; set; }

        public int? DatabaseId { get; set; }

        public ProjectV2FieldConfiguration Field { get; set; }

        public ID Id { get; set; }

        public ProjectV2Item Item { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}