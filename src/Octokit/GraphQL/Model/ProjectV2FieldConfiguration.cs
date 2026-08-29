namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Configurations for project fields.
    /// </summary>
    public class ProjectV2FieldConfiguration : QueryableValue<ProjectV2FieldConfiguration>, IUnion
    {
        internal ProjectV2FieldConfiguration(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// A field inside a project.
            /// </summary>
            public Selector<T> ProjectV2Field(Func<ProjectV2Field, T> selector) => default;

            /// <summary>
            /// An iteration field inside a project.
            /// </summary>
            public Selector<T> ProjectV2IterationField(Func<ProjectV2IterationField, T> selector) => default;

            /// <summary>
            /// A single select field inside a project.
            /// </summary>
            public Selector<T> ProjectV2SingleSelectField(Func<ProjectV2SingleSelectField, T> selector) => default;
        }

        internal static ProjectV2FieldConfiguration Create(Expression expression)
        {
            return new ProjectV2FieldConfiguration(expression);
        }
    }
}