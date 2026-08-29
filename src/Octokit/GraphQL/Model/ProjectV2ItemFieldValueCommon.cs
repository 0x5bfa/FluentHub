namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Common fields across different project field value types
    /// </summary>
    [GraphQLIdentifier("ProjectV2ItemFieldValueCommon")]
    public interface IProjectV2ItemFieldValueCommon : IQueryableValue<IProjectV2ItemFieldValueCommon>, IQueryableInterface
    {
        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The actor who created the item.
        /// </summary>
        IActor Creator { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        long? DatabaseId { get; }

        /// <summary>
        /// The project field that contains this value.
        /// </summary>
        ProjectV2FieldConfiguration Field { get; }

        /// <summary>
        /// The Node ID of the ProjectV2ItemFieldValueCommon object
        /// </summary>
        ID Id { get; }

        /// <summary>
        /// The project item that contains this value.
        /// </summary>
        ProjectV2Item Item { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        DateTimeOffset UpdatedAt { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("ProjectV2ItemFieldValueCommon")]
    internal class StubIProjectV2ItemFieldValueCommon : QueryableValue<StubIProjectV2ItemFieldValueCommon>, IProjectV2ItemFieldValueCommon
    {
        internal StubIProjectV2ItemFieldValueCommon(Expression expression) : base(expression)
        {
        }

        public DateTimeOffset CreatedAt { get; }

        public IActor Creator => this.CreateProperty(x => x.Creator, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        public long? DatabaseId { get; }

        public ProjectV2FieldConfiguration Field => this.CreateProperty(x => x.Field, Octokit.GraphQL.Model.ProjectV2FieldConfiguration.Create);

        public ID Id { get; }

        public ProjectV2Item Item => this.CreateProperty(x => x.Item, Octokit.GraphQL.Model.ProjectV2Item.Create);

        public DateTimeOffset UpdatedAt { get; }

        internal static StubIProjectV2ItemFieldValueCommon Create(Expression expression)
        {
            return new StubIProjectV2ItemFieldValueCommon(expression);
        }
    }
}