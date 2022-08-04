namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents an author of discussions in repositories.
    /// </summary>
    public interface IRepositoryDiscussionAuthor
    {
        /// <summary>
        /// Discussions this user has started.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="answered">Filter discussions to only those that have been answered or not. Defaults to including both answered and unanswered discussions.</param>
        /// <param name="orderBy">Ordering options for discussions returned from the connection.</param>
        /// <param name="repositoryId">Filter discussions to only those in a specific repository.</param>
        DiscussionConnection RepositoryDiscussions { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class RepositoryDiscussionAuthor : IRepositoryDiscussionAuthor
    {
        public DiscussionConnection RepositoryDiscussions { get; set; }
    }
}