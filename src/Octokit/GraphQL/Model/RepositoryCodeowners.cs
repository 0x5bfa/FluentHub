namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Information extracted from a repository's `CODEOWNERS` file.
    /// </summary>
    public class RepositoryCodeowners : QueryableValue<RepositoryCodeowners>
    {
        internal RepositoryCodeowners(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Any problems that were encountered while parsing the `CODEOWNERS` file.
        /// </summary>
        public IQueryableList<RepositoryCodeownersError> Errors => this.CreateProperty(x => x.Errors);

        internal static RepositoryCodeowners Create(Expression expression)
        {
            return new RepositoryCodeowners(expression);
        }
    }
}