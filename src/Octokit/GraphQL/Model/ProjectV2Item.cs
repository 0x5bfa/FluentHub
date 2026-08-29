namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An item within a Project.
    /// </summary>
    public class ProjectV2Item : QueryableValue<ProjectV2Item>
    {
        internal ProjectV2Item(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The content of the referenced draft issue, issue, or pull request
        /// </summary>
        public ProjectV2ItemContent Content => this.CreateProperty(x => x.Content, Octokit.GraphQL.Model.ProjectV2ItemContent.Create);

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
        /// The field value of the first project field which matches the 'name' argument that is set on the item.
        /// </summary>
        /// <param name="name">The name of the field to return the field value of</param>
        public ProjectV2ItemFieldValue FieldValueByName(Arg<string> name) => this.CreateMethodCall(x => x.FieldValueByName(name), Octokit.GraphQL.Model.ProjectV2ItemFieldValue.Create);

        /// <summary>
        /// The field values that are set on the item.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for project v2 item field values returned from the connection</param>
        public ProjectV2ItemFieldValueConnection FieldValues(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ProjectV2ItemFieldValueOrder>? orderBy = null) => this.CreateMethodCall(x => x.FieldValues(first, after, last, before, orderBy), Octokit.GraphQL.Model.ProjectV2ItemFieldValueConnection.Create);

        /// <summary>
        /// Identifies the primary key from the database as a BigInt.
        /// </summary>
        public string FullDatabaseId { get; }

        /// <summary>
        /// The Node ID of the ProjectV2Item object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Whether the item is archived.
        /// </summary>
        public bool IsArchived { get; }

        /// <summary>
        /// The project that contains this item.
        /// </summary>
        public ProjectV2 Project => this.CreateProperty(x => x.Project, Octokit.GraphQL.Model.ProjectV2.Create);

        /// <summary>
        /// The type of the item.
        /// </summary>
        public ProjectV2ItemType Type { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static ProjectV2Item Create(Expression expression)
        {
            return new ProjectV2Item(expression);
        }
    }
}