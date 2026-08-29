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
    /// Represents a type that can be required by a pull request for merging.
    /// </summary>
    [GraphQLIdentifier("RequirableByPullRequest")]
    public interface IRequirableByPullRequest : IQueryableValue<IRequirableByPullRequest>, IQueryableInterface
    {
        /// <summary>
        /// Whether this is required to pass before merging for a specific pull request.
        /// </summary>
        /// <param name="pullRequestId">The id of the pull request this is required for</param>
        /// <param name="pullRequestNumber">The number of the pull request this is required for</param>
        bool IsRequired(Arg<ID>? pullRequestId = null, Arg<int>? pullRequestNumber = null);
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("RequirableByPullRequest")]
    internal class StubIRequirableByPullRequest : QueryableValue<StubIRequirableByPullRequest>, IRequirableByPullRequest
    {
        internal StubIRequirableByPullRequest(Expression expression) : base(expression)
        {
        }

        public bool IsRequired(Arg<ID>? pullRequestId = null, Arg<int>? pullRequestNumber = null) => default;

        internal static StubIRequirableByPullRequest Create(Expression expression)
        {
            return new StubIRequirableByPullRequest(expression);
        }
    }
}