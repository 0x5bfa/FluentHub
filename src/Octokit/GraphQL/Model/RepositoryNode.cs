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
    /// Represents a object that belongs to a repository.
    /// </summary>
    [GraphQLIdentifier("RepositoryNode")]
    public interface IRepositoryNode : IQueryableValue<IRepositoryNode>, IQueryableInterface
    {
        /// <summary>
        /// The repository associated with this node.
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

    [GraphQLIdentifier("RepositoryNode")]
    internal class StubIRepositoryNode : QueryableValue<StubIRepositoryNode>, IRepositoryNode
    {
        internal StubIRepositoryNode(Expression expression) : base(expression)
        {
        }

        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static StubIRepositoryNode Create(Expression expression)
        {
            return new StubIRepositoryNode(expression);
        }
    }
}