// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A topic aggregates entities that are related to a subject.
	/// </summary>
	public class Topic
	{
		public ID Id { get; set; }

		/// <summary>
		/// The topic's name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// A list of related topics, including aliases of this topic, sorted with the most relevant
		/// first. Returns up to 10 Topics.
		/// </summary>
		/// <param name="first">How many topics to return.</param>
		public List<Topic> RelatedTopics { get; set; }

		/// <summary>
		/// A list of repositories.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="affiliations">Array of viewer's affiliation options for repositories returned from the connection. For example, OWNER will include only repositories that the current viewer owns.</param>
		/// <param name="hasIssuesEnabled">If non-null, filters repositories according to whether they have issues enabled</param>
		/// <param name="isLocked">If non-null, filters repositories according to whether they have been locked</param>
		/// <param name="orderBy">Ordering options for repositories returned from the connection</param>
		/// <param name="ownerAffiliations">Array of owner's affiliation options for repositories returned from the connection. For example, OWNER will include only repositories that the organization or user being viewed owns.</param>
		/// <param name="privacy">If non-null, filters repositories according to privacy</param>
		/// <param name="sponsorableOnly">If true, only repositories whose owner can be sponsored via GitHub Sponsors will be returned.</param>
		public RepositoryConnection Repositories { get; set; }

		/// <summary>
		/// Returns a count of how many stargazers there are on this object
		/// </summary>
		public int StargazerCount { get; set; }

		/// <summary>
		/// A list of users who have starred this starrable.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Order for connection</param>
		public StargazerConnection Stargazers { get; set; }

		/// <summary>
		/// Returns a boolean indicating whether the viewing user has starred this starrable.
		/// </summary>
		public bool ViewerHasStarred { get; set; }
	}
}
