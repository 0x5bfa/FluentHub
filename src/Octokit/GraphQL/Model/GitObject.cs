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
    /// Represents a Git object.
    /// </summary>
    [GraphQLIdentifier("GitObject")]
    public interface IGitObject : IQueryableValue<IGitObject>, IQueryableInterface
    {
        /// <summary>
        /// An abbreviated version of the Git object ID
        /// </summary>
        string AbbreviatedOid { get; }

        /// <summary>
        /// The HTTP path for this Git object
        /// </summary>
        string CommitResourcePath { get; }

        /// <summary>
        /// The HTTP URL for this Git object
        /// </summary>
        string CommitUrl { get; }

        /// <summary>
        /// The Node ID of the GitObject object
        /// </summary>
        ID Id { get; }

        /// <summary>
        /// The Git object ID
        /// </summary>
        string Oid { get; }

        /// <summary>
        /// The Repository the Git object belongs to
        /// </summary>
        Repository Repository { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("GitObject")]
    internal class StubIGitObject : QueryableValue<StubIGitObject>, IGitObject
    {
        internal StubIGitObject(Expression expression) : base(expression)
        {
        }

        public string AbbreviatedOid { get; }

        public string CommitResourcePath { get; }

        public string CommitUrl { get; }

        public ID Id { get; }

        public string Oid { get; }

        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static StubIGitObject Create(Expression expression)
        {
            return new StubIGitObject(expression);
        }
    }
}