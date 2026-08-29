namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A Dependabot Update for a dependency in a repository
    /// </summary>
    public class DependabotUpdate : QueryableValue<DependabotUpdate>
    {
        internal DependabotUpdate(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The error from a dependency update
        /// </summary>
        public DependabotUpdateError Error => this.CreateProperty(x => x.Error, Octokit.GraphQL.Model.DependabotUpdateError.Create);

        /// <summary>
        /// The associated pull request
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        /// <summary>
        /// The repository associated with this node.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static DependabotUpdate Create(Expression expression)
        {
            return new DependabotUpdate(expression);
        }
    }
}