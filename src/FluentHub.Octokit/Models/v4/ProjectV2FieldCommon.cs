namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Common fields across different project field types
    /// </summary>
    public interface IProjectV2FieldCommon
    {        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The field's type.
        /// </summary>
        ProjectV2FieldType DataType { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        int? DatabaseId { get; set; }

        ID Id { get; set; }

        /// <summary>
        /// The project field's name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The project that contains this field.
        /// </summary>
        ProjectV2 Project { get; set; }

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

    public class ProjectV2FieldCommon : IProjectV2FieldCommon
    {
        public DateTimeOffset CreatedAt { get; set; }

        public ProjectV2FieldType DataType { get; set; }

        public int? DatabaseId { get; set; }

        public ID Id { get; set; }

        public string Name { get; set; }

        public ProjectV2 Project { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}