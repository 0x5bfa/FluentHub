namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A repository pull request template.
    /// </summary>
    public class PullRequestTemplate : QueryableValue<PullRequestTemplate>
    {
        internal PullRequestTemplate(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The body of the template
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The filename of the template
        /// </summary>
        public string Filename { get; }

        /// <summary>
        /// The repository the template belongs to
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static PullRequestTemplate Create(Expression expression)
        {
            return new PullRequestTemplate(expression);
        }
    }
}