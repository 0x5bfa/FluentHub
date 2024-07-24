// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

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
		/// <param name="states">A list of states to filter the discussions by.</param>
		DiscussionConnection RepositoryDiscussions { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class RepositoryDiscussionAuthor : IRepositoryDiscussionAuthor
	{
		public DiscussionConnection RepositoryDiscussions { get; set; }
	}
}

