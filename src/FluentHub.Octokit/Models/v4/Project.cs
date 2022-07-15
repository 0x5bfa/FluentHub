namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Projects manage issues, pull requests and notes within a project owner.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// The project's description body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The projects description body rendered to HTML.
        /// </summary>
        public string BodyHTML { get; set; }

        /// <summary>
        /// `true` if the object is closed (definition of closed may depend on type)
        /// </summary>
        public bool Closed { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was closed.
        /// </summary>
        public DateTimeOffset? ClosedAt { get; set; }

        /// <summary>
        /// List of columns in the project
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ProjectColumnConnection Columns { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The actor who originally created the project.
        /// </summary>
        public IActor Creator { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The project's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The project's number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// The project's owner. Currently limited to repositories, organizations, and users.
        /// </summary>
        public IProjectOwner Owner { get; set; }

        /// <summary>
        /// List of pending cards in this project
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="archivedStates">A list of archived states to filter the cards by</param>
        public ProjectCardConnection PendingCards { get; set; }

        /// <summary>
        /// Project progress details.
        /// </summary>
        public ProjectProgress Progress { get; set; }

        /// <summary>
        /// The HTTP path for this project
        /// </summary>
        public string ResourcePath { get; set; }

        /// <summary>
        /// Whether the project is open or closed.
        /// </summary>
        public ProjectState State { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// The HTTP URL for this project
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Check if the current viewer can update this object.
        /// </summary>
        public bool ViewerCanUpdate { get; set; }
    }
}