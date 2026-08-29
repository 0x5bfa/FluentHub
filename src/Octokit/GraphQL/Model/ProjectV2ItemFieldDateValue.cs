namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The value of a date field in a Project item.
    /// </summary>
    public class ProjectV2ItemFieldDateValue : QueryableValue<ProjectV2ItemFieldDateValue>
    {
        internal ProjectV2ItemFieldDateValue(Expression expression) : base(expression)
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
        /// Date value for the field
        /// </summary>
        public string Date { get; }

        /// <summary>
        /// The project field that contains this value.
        /// </summary>
        public ProjectV2FieldConfiguration Field => this.CreateProperty(x => x.Field, Octokit.GraphQL.Model.ProjectV2FieldConfiguration.Create);

        /// <summary>
        /// The Node ID of the ProjectV2ItemFieldDateValue object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The project item that contains this value.
        /// </summary>
        public ProjectV2Item Item => this.CreateProperty(x => x.Item, Octokit.GraphQL.Model.ProjectV2Item.Create);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static ProjectV2ItemFieldDateValue Create(Expression expression)
        {
            return new ProjectV2ItemFieldDateValue(expression);
        }
    }
}