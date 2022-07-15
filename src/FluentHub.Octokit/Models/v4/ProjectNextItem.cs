namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An item within a new Project.
    /// </summary>
    public class ProjectNextItem
    {
        /// <summary>
        /// The content of the referenced draft issue, issue, or pull request
        /// </summary>
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public ProjectNextItemContent Content { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The actor who created the item.
        /// </summary>
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public IActor Creator { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public int? DatabaseId { get; set; }

        /// <summary>
        /// List of field values
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ProjectNextItemFieldValueConnection FieldValues { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Whether the item is archived.
        /// </summary>
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public bool IsArchived { get; set; }

        /// <summary>
        /// The project that contains this item.
        /// </summary>
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public ProjectNext Project { get; set; }

        /// <summary>
        /// The title of the item
        /// </summary>
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public string Title { get; set; }

        /// <summary>
        /// The type of the item.
        /// </summary>
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public ProjectItemType Type { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}