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
    /// A subject that may be upvoted.
    /// </summary>
    [GraphQLIdentifier("Votable")]
    public interface IVotable : IQueryableValue<IVotable>, IQueryableInterface
    {
        /// <summary>
        /// Number of upvotes that this subject has received.
        /// </summary>
        int UpvoteCount { get; }

        /// <summary>
        /// Whether or not the current user can add or remove an upvote on this subject.
        /// </summary>
        bool ViewerCanUpvote { get; }

        /// <summary>
        /// Whether or not the current user has already upvoted this subject.
        /// </summary>
        bool ViewerHasUpvoted { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("Votable")]
    internal class StubIVotable : QueryableValue<StubIVotable>, IVotable
    {
        internal StubIVotable(Expression expression) : base(expression)
        {
        }

        public int UpvoteCount { get; }

        public bool ViewerCanUpvote { get; }

        public bool ViewerHasUpvoted { get; }

        internal static StubIVotable Create(Expression expression)
        {
            return new StubIVotable(expression);
        }
    }
}