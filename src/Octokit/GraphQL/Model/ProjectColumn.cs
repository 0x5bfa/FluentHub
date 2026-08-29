namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A column inside a project.
    /// </summary>
    public class ProjectColumn : QueryableValue<ProjectColumn>
    {
        internal ProjectColumn(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// List of cards in the column
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="archivedStates">A list of archived states to filter the cards by</param>
        public ProjectCardConnection Cards(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<ProjectCardArchivedState?>>? archivedStates = null) => this.CreateMethodCall(x => x.Cards(first, after, last, before, archivedStates), Octokit.GraphQL.Model.ProjectCardConnection.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The Node ID of the ProjectColumn object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The project column's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The project that contains this column.
        /// </summary>
        public Project Project => this.CreateProperty(x => x.Project, Octokit.GraphQL.Model.Project.Create);

        /// <summary>
        /// The semantic purpose of the column
        /// </summary>
        public ProjectColumnPurpose? Purpose { get; }

        /// <summary>
        /// The HTTP path for this project column
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this project column
        /// </summary>
        public string Url { get; }

        internal static ProjectColumn Create(Expression expression)
        {
            return new ProjectColumn(expression);
        }
    }
}