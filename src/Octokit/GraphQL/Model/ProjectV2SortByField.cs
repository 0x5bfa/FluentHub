namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a sort by field and direction.
    /// </summary>
    public class ProjectV2SortByField : QueryableValue<ProjectV2SortByField>
    {
        internal ProjectV2SortByField(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The direction of the sorting. Possible values are ASC and DESC.
        /// </summary>
        public OrderDirection Direction { get; }

        /// <summary>
        /// The field by which items are sorted.
        /// </summary>
        public ProjectV2FieldConfiguration Field => this.CreateProperty(x => x.Field, Octokit.GraphQL.Model.ProjectV2FieldConfiguration.Create);

        internal static ProjectV2SortByField Create(Expression expression)
        {
            return new ProjectV2SortByField(expression);
        }
    }
}