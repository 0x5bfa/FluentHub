namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a Git tag.
    /// </summary>
    public class Tag : QueryableValue<Tag>
    {
        internal Tag(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// An abbreviated version of the Git object ID
        /// </summary>
        public string AbbreviatedOid { get; }

        /// <summary>
        /// The HTTP path for this Git object
        /// </summary>
        public string CommitResourcePath { get; }

        /// <summary>
        /// The HTTP URL for this Git object
        /// </summary>
        public string CommitUrl { get; }

        /// <summary>
        /// The Node ID of the Tag object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The Git tag message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// The Git tag name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The Git object ID
        /// </summary>
        public string Oid { get; }

        /// <summary>
        /// The Repository the Git object belongs to
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// Details about the tag author.
        /// </summary>
        public GitActor Tagger => this.CreateProperty(x => x.Tagger, Octokit.GraphQL.Model.GitActor.Create);

        /// <summary>
        /// The Git object the tag points to.
        /// </summary>
        public IGitObject Target => this.CreateProperty(x => x.Target, Octokit.GraphQL.Model.Internal.StubIGitObject.Create);

        internal static Tag Create(Expression expression)
        {
            return new Tag(expression);
        }
    }
}