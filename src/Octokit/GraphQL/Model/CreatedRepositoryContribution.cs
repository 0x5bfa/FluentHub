namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents the contribution a user made on GitHub by creating a repository.
    /// </summary>
    public class CreatedRepositoryContribution : QueryableValue<CreatedRepositoryContribution>
    {
        internal CreatedRepositoryContribution(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Whether this contribution is associated with a record you do not have access to. For
        /// example, your own 'first issue' contribution may have been made on a repository you can no
        /// longer access.
        /// </summary>
        public bool IsRestricted { get; }

        /// <summary>
        /// When this contribution was made.
        /// </summary>
        public DateTimeOffset OccurredAt { get; }

        /// <summary>
        /// The repository that was created.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The HTTP path for this contribution.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The HTTP URL for this contribution.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// The user who made this contribution.
        /// </summary>
        public User User => this.CreateProperty(x => x.User, Octokit.GraphQL.Model.User.Create);

        internal static CreatedRepositoryContribution Create(Expression expression)
        {
            return new CreatedRepositoryContribution(expression);
        }
    }
}