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
    /// Represents an author of discussion comments in repositories.
    /// </summary>
    [GraphQLIdentifier("RepositoryDiscussionCommentAuthor")]
    public interface IRepositoryDiscussionCommentAuthor : IQueryableValue<IRepositoryDiscussionCommentAuthor>, IQueryableInterface
    {
        /// <summary>
        /// Discussion comments this user has authored.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="onlyAnswers">Filter discussion comments to only those that were marked as the answer</param>
        /// <param name="repositoryId">Filter discussion comments to only those in a specific repository.</param>
        DiscussionCommentConnection RepositoryDiscussionComments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? onlyAnswers = null, Arg<ID>? repositoryId = null);
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("RepositoryDiscussionCommentAuthor")]
    internal class StubIRepositoryDiscussionCommentAuthor : QueryableValue<StubIRepositoryDiscussionCommentAuthor>, IRepositoryDiscussionCommentAuthor
    {
        internal StubIRepositoryDiscussionCommentAuthor(Expression expression) : base(expression)
        {
        }

        public DiscussionCommentConnection RepositoryDiscussionComments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? onlyAnswers = null, Arg<ID>? repositoryId = null) => this.CreateMethodCall(x => x.RepositoryDiscussionComments(first, after, last, before, onlyAnswers, repositoryId), Octokit.GraphQL.Model.DiscussionCommentConnection.Create);

        internal static StubIRepositoryDiscussionCommentAuthor Create(Expression expression)
        {
            return new StubIRepositoryDiscussionCommentAuthor(expression);
        }
    }
}