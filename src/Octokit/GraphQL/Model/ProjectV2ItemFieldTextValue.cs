namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The value of a text field in a Project item.
    /// </summary>
    public class ProjectV2ItemFieldTextValue : QueryableValue<ProjectV2ItemFieldTextValue>
    {
        internal ProjectV2ItemFieldTextValue(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The actor who created the item.
        /// </summary>
        public IActor Creator => this.CreateProperty(x => x.Creator, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The project field that contains this value.
        /// </summary>
        public ProjectV2FieldConfiguration Field => this.CreateProperty(x => x.Field, Octokit.GraphQL.Model.ProjectV2FieldConfiguration.Create);

        /// <summary>
        /// The Node ID of the ProjectV2ItemFieldTextValue object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The project item that contains this value.
        /// </summary>
        public ProjectV2Item Item => this.CreateProperty(x => x.Item, Octokit.GraphQL.Model.ProjectV2Item.Create);

        /// <summary>
        /// Text value of a field
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static ProjectV2ItemFieldTextValue Create(Expression expression)
        {
            return new ProjectV2ItemFieldTextValue(expression);
        }
    }
}