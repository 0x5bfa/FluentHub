namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An iteration field inside a project.
    /// </summary>
    public class ProjectV2IterationField : QueryableValue<ProjectV2IterationField>
    {
        internal ProjectV2IterationField(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Iteration configuration settings
        /// </summary>
        public ProjectV2IterationFieldConfiguration Configuration => this.CreateProperty(x => x.Configuration, Octokit.GraphQL.Model.ProjectV2IterationFieldConfiguration.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The field's type.
        /// </summary>
        public ProjectV2FieldType DataType { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The Node ID of the ProjectV2IterationField object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The project field's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The project that contains this field.
        /// </summary>
        public ProjectV2 Project => this.CreateProperty(x => x.Project, Octokit.GraphQL.Model.ProjectV2.Create);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static ProjectV2IterationField Create(Expression expression)
        {
            return new ProjectV2IterationField(expression);
        }
    }
}