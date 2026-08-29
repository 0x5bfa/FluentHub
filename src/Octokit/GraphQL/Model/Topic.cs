namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A topic aggregates entities that are related to a subject.
    /// </summary>
    public class Topic : QueryableValue<Topic>
    {
        internal Topic(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The Node ID of the Topic object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The topic's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A list of related topics, including aliases of this topic, sorted with the most relevant
        /// first. Returns up to 10 Topics.
        /// </summary>
        /// <param name="first">How many topics to return.</param>
        public IQueryableList<Topic> RelatedTopics(Arg<int>? first = null) => this.CreateMethodCall(x => x.RelatedTopics(first));

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
        /// <param name="privacy">If non-null, filters repositories according to privacy. Internal repositories are considered private; consider using the visibility argument if only internal repositories are needed. Cannot be combined with the visibility argument.</param>
        /// <param name="sponsorableOnly">If true, only repositories whose owner can be sponsored via GitHub Sponsors will be returned.</param>
        /// <param name="visibility">If non-null, filters repositories according to visibility. Cannot be combined with the privacy argument.</param>
        public RepositoryConnection Repositories(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<RepositoryAffiliation?>>? affiliations = null, Arg<bool>? hasIssuesEnabled = null, Arg<bool>? isLocked = null, Arg<RepositoryOrder>? orderBy = null, Arg<IEnumerable<RepositoryAffiliation?>>? ownerAffiliations = null, Arg<RepositoryPrivacy>? privacy = null, Arg<bool>? sponsorableOnly = null, Arg<RepositoryVisibility>? visibility = null) => this.CreateMethodCall(x => x.Repositories(first, after, last, before, affiliations, hasIssuesEnabled, isLocked, orderBy, ownerAffiliations, privacy, sponsorableOnly, visibility), Octokit.GraphQL.Model.RepositoryConnection.Create);

        /// <summary>
        /// Returns a count of how many stargazers there are on this object
        /// </summary>
        public int StargazerCount { get; }

        /// <summary>
        /// A list of users who have starred this starrable.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Order for connection</param>
        public StargazerConnection Stargazers(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<StarOrder>? orderBy = null) => this.CreateMethodCall(x => x.Stargazers(first, after, last, before, orderBy), Octokit.GraphQL.Model.StargazerConnection.Create);

        /// <summary>
        /// Returns a boolean indicating whether the viewing user has starred this starrable.
        /// </summary>
        public bool ViewerHasStarred { get; }

        internal static Topic Create(Expression expression)
        {
            return new Topic(expression);
        }
    }
}