namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Projects manage issues, pull requests and notes within a project owner.
    /// </summary>
    public class Project : QueryableValue<Project>
    {
        internal Project(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The project's description body.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The projects description body rendered to HTML.
        /// </summary>
        public string BodyHTML { get; }

        /// <summary>
        /// Indicates if the object is closed (definition of closed may depend on type)
        /// </summary>
        public bool Closed { get; }

        /// <summary>
        /// Identifies the date and time when the object was closed.
        /// </summary>
        public DateTimeOffset? ClosedAt { get; }

        /// <summary>
        /// List of columns in the project
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ProjectColumnConnection Columns(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Columns(first, after, last, before), Octokit.GraphQL.Model.ProjectColumnConnection.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The actor who originally created the project.
        /// </summary>
        public IActor Creator => this.CreateProperty(x => x.Creator, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The Node ID of the Project object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The project's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The project's number.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// The project's owner. Currently limited to repositories, organizations, and users.
        /// </summary>
        public IProjectOwner Owner => this.CreateProperty(x => x.Owner, Octokit.GraphQL.Model.Internal.StubIProjectOwner.Create);

        /// <summary>
        /// List of pending cards in this project
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="archivedStates">A list of archived states to filter the cards by</param>
        public ProjectCardConnection PendingCards(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<ProjectCardArchivedState?>>? archivedStates = null) => this.CreateMethodCall(x => x.PendingCards(first, after, last, before, archivedStates), Octokit.GraphQL.Model.ProjectCardConnection.Create);

        /// <summary>
        /// Project progress details.
        /// </summary>
        public ProjectProgress Progress => this.CreateProperty(x => x.Progress, Octokit.GraphQL.Model.ProjectProgress.Create);

        /// <summary>
        /// The HTTP path for this project
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Whether the project is open or closed.
        /// </summary>
        public ProjectState State { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this project
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Indicates if the object can be closed by the viewer.
        /// </summary>
        public bool ViewerCanClose { get; }

        /// <summary>
        /// Indicates if the object can be reopened by the viewer.
        /// </summary>
        public bool ViewerCanReopen { get; }

        /// <summary>
        /// Check if the current viewer can update this object.
        /// </summary>
        public bool ViewerCanUpdate { get; }

        internal static Project Create(Expression expression)
        {
            return new Project(expression);
        }
    }
}