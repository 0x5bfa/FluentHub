namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A draft issue within a project.
    /// </summary>
    public class DraftIssue
    {
        /// <summary>
        /// A list of users to assigned to this draft issue.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserConnection Assignees { get; set; }

        /// <summary>
        /// The body of the draft issue.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The body of the draft issue rendered to HTML.
        /// </summary>
        public string BodyHTML { get; set; }

        /// <summary>
        /// The body of the draft issue rendered to text.
        /// </summary>
        public string BodyText { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The actor who created this draft issue.
        /// </summary>
        public IActor Creator { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The project (beta) that contains this draft issue.
        /// </summary>
        public ProjectNext Project { get; set; }

        /// <summary>
        /// The project (beta) item that wraps this draft issue.
        /// </summary>
        public ProjectNextItem ProjectItem { get; set; }

        /// <summary>
        /// List of items linked with the draft issue (currently draft issue can be linked to only one item).
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ProjectV2ItemConnection ProjectV2Items { get; set; }

        /// <summary>
        /// Projects that link to this draft issue (currently draft issue can be linked to only one project).
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ProjectV2Connection ProjectsV2 { get; set; }

        /// <summary>
        /// The title of the draft issue
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }
    }
}