namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A card in a project.
    /// </summary>
    public class ProjectCard : QueryableValue<ProjectCard>
    {
        internal ProjectCard(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The project column this card is associated under. A card may only belong to one
        /// project column at a time. The column field will be null if the card is created
        /// in a pending state and has yet to be associated with a column. Once cards are
        /// associated with a column, they will not become pending in the future.
        /// </summary>
        public ProjectColumn Column => this.CreateProperty(x => x.Column, Octokit.GraphQL.Model.ProjectColumn.Create);

        /// <summary>
        /// The card content item
        /// </summary>
        public ProjectCardItem Content => this.CreateProperty(x => x.Content, Octokit.GraphQL.Model.ProjectCardItem.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The actor who created this card
        /// </summary>
        public IActor Creator => this.CreateProperty(x => x.Creator, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The Node ID of the ProjectCard object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Whether the card is archived
        /// </summary>
        public bool IsArchived { get; }

        /// <summary>
        /// The card note
        /// </summary>
        public string Note { get; }

        /// <summary>
        /// The project that contains this card.
        /// </summary>
        public Project Project => this.CreateProperty(x => x.Project, Octokit.GraphQL.Model.Project.Create);

        /// <summary>
        /// The HTTP path for this card
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The state of ProjectCard
        /// </summary>
        public ProjectCardState? State { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this card
        /// </summary>
        public string Url { get; }

        internal static ProjectCard Create(Expression expression)
        {
            return new ProjectCard(expression);
        }
    }
}