namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The value of a repository field in a Project item.
    /// </summary>
    public class ProjectV2ItemFieldRepositoryValue : QueryableValue<ProjectV2ItemFieldRepositoryValue>
    {
        internal ProjectV2ItemFieldRepositoryValue(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The field that contains this value.
        /// </summary>
        public ProjectV2FieldConfiguration Field => this.CreateProperty(x => x.Field, Octokit.GraphQL.Model.ProjectV2FieldConfiguration.Create);

        /// <summary>
        /// The repository for this field.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static ProjectV2ItemFieldRepositoryValue Create(Expression expression)
        {
            return new ProjectV2ItemFieldRepositoryValue(expression);
        }
    }
}