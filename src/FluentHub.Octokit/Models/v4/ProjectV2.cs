namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// New projects that manage issues, pull requests and drafts using tables and boards.
    /// </summary>
    public class ProjectV2
    {
        /// <summary>
        /// Returns true if the project is closed.
        /// </summary>
        public bool Closed { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was closed.
        /// </summary>
        public DateTimeOffset? ClosedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was closed."
        /// <summary>
        public string ClosedAtHumanized { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was created."
        /// <summary>
        public string CreatedAtHumanized { get; set; }

        /// <summary>
        /// The actor who originally created the project.
        /// </summary>
        public IActor Creator { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; set; }

        /// <summary>
        /// A field of the project
        /// </summary>
        /// <param name="name">The name of the field</param>
        public ProjectV2FieldConfiguration Field { get; set; }

        /// <summary>
        /// List of fields and their constraints in the project
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for project v2 fields returned from the connection</param>
        public ProjectV2FieldConfigurationConnection Fields { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// List of items in the project
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for project v2 items returned from the connection</param>
        public ProjectV2ItemConnection Items { get; set; }

        /// <summary>
        /// The project's number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// The project's owner. Currently limited to organizations and users.
        /// </summary>
        public IProjectV2Owner Owner { get; set; }

        /// <summary>
        /// Returns true if the project is public.
        /// </summary>
        public bool Public { get; set; }

        /// <summary>
        /// The project's readme.
        /// </summary>
        public string Readme { get; set; }

        /// <summary>
        /// The repositories the project is linked to.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for repositories returned from the connection</param>
        public RepositoryConnection Repositories { get; set; }

        /// <summary>
        /// The HTTP path for this project
        /// </summary>
        public string ResourcePath { get; set; }

        /// <summary>
        /// The project's short description.
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// The teams the project is linked to.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for teams returned from this connection.</param>
        public TeamConnection Teams { get; set; }

        /// <summary>
        /// Returns true if this project is a template.
        /// </summary>
        public bool Template { get; set; }

        /// <summary>
        /// The project's name.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was last updated."
        /// <summary>
        public string UpdatedAtHumanized { get; set; }

        /// <summary>
        /// The HTTP URL for this project
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// A view of the project
        /// </summary>
        /// <param name="number">The number of a view belonging to the project</param>
        public ProjectV2View View { get; set; }

        /// <summary>
        /// Indicates if the object can be closed by the viewer.
        /// </summary>
        public bool ViewerCanClose { get; set; }

        /// <summary>
        /// Indicates if the object can be reopened by the viewer.
        /// </summary>
        public bool ViewerCanReopen { get; set; }

        /// <summary>
        /// Check if the current viewer can update this object.
        /// </summary>
        public bool ViewerCanUpdate { get; set; }

        /// <summary>
        /// List of views in the project
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for project v2 views returned from the connection</param>
        public ProjectV2ViewConnection Views { get; set; }

        /// <summary>
        /// A workflow of the project
        /// </summary>
        /// <param name="number">The number of a workflow belonging to the project</param>
        public ProjectV2Workflow Workflow { get; set; }

        /// <summary>
        /// List of the workflows in the project
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for project v2 workflows returned from the connection</param>
        public ProjectV2WorkflowConnection Workflows { get; set; }
    }
}