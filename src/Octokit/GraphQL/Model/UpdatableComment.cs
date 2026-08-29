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
    /// Comments that can be updated.
    /// </summary>
    [GraphQLIdentifier("UpdatableComment")]
    public interface IUpdatableComment : IQueryableValue<IUpdatableComment>, IQueryableInterface
    {
        /// <summary>
        /// Reasons why the current viewer can not update this comment.
        /// </summary>
        IEnumerable<CommentCannotUpdateReason> ViewerCannotUpdateReasons { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("UpdatableComment")]
    internal class StubIUpdatableComment : QueryableValue<StubIUpdatableComment>, IUpdatableComment
    {
        internal StubIUpdatableComment(Expression expression) : base(expression)
        {
        }

        public IEnumerable<CommentCannotUpdateReason> ViewerCannotUpdateReasons { get; }

        internal static StubIUpdatableComment Create(Expression expression)
        {
            return new StubIUpdatableComment(expression);
        }
    }
}