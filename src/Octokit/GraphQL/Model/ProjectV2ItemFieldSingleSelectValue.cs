namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The value of a single select field in a Project item.
    /// </summary>
    public class ProjectV2ItemFieldSingleSelectValue : QueryableValue<ProjectV2ItemFieldSingleSelectValue>
    {
        internal ProjectV2ItemFieldSingleSelectValue(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The color applied to the selected single-select option.
        /// </summary>
        public ProjectV2SingleSelectFieldOptionColor Color { get; }

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
        /// A plain-text description of the selected single-select option, such as what the option means.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The description of the selected single-select option, including HTML tags.
        /// </summary>
        public string DescriptionHTML { get; }

        /// <summary>
        /// The project field that contains this value.
        /// </summary>
        public ProjectV2FieldConfiguration Field => this.CreateProperty(x => x.Field, Octokit.GraphQL.Model.ProjectV2FieldConfiguration.Create);

        /// <summary>
        /// The Node ID of the ProjectV2ItemFieldSingleSelectValue object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The project item that contains this value.
        /// </summary>
        public ProjectV2Item Item => this.CreateProperty(x => x.Item, Octokit.GraphQL.Model.ProjectV2Item.Create);

        /// <summary>
        /// The name of the selected single select option.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The html name of the selected single select option.
        /// </summary>
        public string NameHTML { get; }

        /// <summary>
        /// The id of the selected single select option.
        /// </summary>
        public string OptionId { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static ProjectV2ItemFieldSingleSelectValue Create(Expression expression)
        {
            return new ProjectV2ItemFieldSingleSelectValue(expression);
        }
    }
}