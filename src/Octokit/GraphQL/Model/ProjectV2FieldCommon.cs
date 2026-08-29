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
    /// Common fields across different project field types
    /// </summary>
    [GraphQLIdentifier("ProjectV2FieldCommon")]
    public interface IProjectV2FieldCommon : IQueryableValue<IProjectV2FieldCommon>, IQueryableInterface
    {
        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The field's type.
        /// </summary>
        ProjectV2FieldType DataType { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        long? DatabaseId { get; }

        /// <summary>
        /// The Node ID of the ProjectV2FieldCommon object
        /// </summary>
        ID Id { get; }

        /// <summary>
        /// The project field's name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The project that contains this field.
        /// </summary>
        ProjectV2 Project { get; }

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

    [GraphQLIdentifier("ProjectV2FieldCommon")]
    internal class StubIProjectV2FieldCommon : QueryableValue<StubIProjectV2FieldCommon>, IProjectV2FieldCommon
    {
        internal StubIProjectV2FieldCommon(Expression expression) : base(expression)
        {
        }

        public DateTimeOffset CreatedAt { get; }

        public ProjectV2FieldType DataType { get; }

        public long? DatabaseId { get; }

        public ID Id { get; }

        public string Name { get; }

        public ProjectV2 Project => this.CreateProperty(x => x.Project, Octokit.GraphQL.Model.ProjectV2.Create);

        public DateTimeOffset UpdatedAt { get; }

        internal static StubIProjectV2FieldCommon Create(Expression expression)
        {
            return new StubIProjectV2FieldCommon(expression);
        }
    }
}