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
    /// Represents an owner of a Repository.
    /// </summary>
    [GraphQLIdentifier("RepositoryOwner")]
    public interface IRepositoryOwner : IQueryableValue<IRepositoryOwner>, IQueryableInterface
    {
        /// <summary>
        /// A URL pointing to the owner's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        string AvatarUrl(Arg<int>? size = null);

        /// <summary>
        /// The Node ID of the RepositoryOwner object
        /// </summary>
        ID Id { get; }

        /// <summary>
        /// The username used to login.
        /// </summary>
        string Login { get; }

        /// <summary>
        /// A list of repositories that the user owns.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="affiliations">Array of viewer's affiliation options for repositories returned from the connection. For example, OWNER will include only repositories that the current viewer owns.</param>
        /// <param name="hasIssuesEnabled">If non-null, filters repositories according to whether they have issues enabled</param>
        /// <param name="isArchived">If non-null, filters repositories according to whether they are archived and not maintained</param>
        /// <param name="isFork">If non-null, filters repositories according to whether they are forks of another repository</param>
        /// <param name="isLocked">If non-null, filters repositories according to whether they have been locked</param>
        /// <param name="orderBy">Ordering options for repositories returned from the connection</param>
        /// <param name="ownerAffiliations">Array of owner's affiliation options for repositories returned from the connection. For example, OWNER will include only repositories that the organization or user being viewed owns.</param>
        /// <param name="privacy">If non-null, filters repositories according to privacy. Internal repositories are considered private; consider using the visibility argument if only internal repositories are needed. Cannot be combined with the visibility argument.</param>
        /// <param name="visibility">If non-null, filters repositories according to visibility. Cannot be combined with the privacy argument.</param>
        RepositoryConnection Repositories(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<RepositoryAffiliation?>>? affiliations = null, Arg<bool>? hasIssuesEnabled = null, Arg<bool>? isArchived = null, Arg<bool>? isFork = null, Arg<bool>? isLocked = null, Arg<RepositoryOrder>? orderBy = null, Arg<IEnumerable<RepositoryAffiliation?>>? ownerAffiliations = null, Arg<RepositoryPrivacy>? privacy = null, Arg<RepositoryVisibility>? visibility = null);

        /// <summary>
        /// Find Repository.
        /// </summary>
        /// <param name="name">Name of Repository to find.</param>
        /// <param name="followRenames">Follow repository renames. If disabled, a repository referenced by its old name will return an error.</param>
        Repository Repository(Arg<string> name, Arg<bool>? followRenames = null);

        /// <summary>
        /// The HTTP URL for the owner.
        /// </summary>
        string ResourcePath { get; }

        /// <summary>
        /// The HTTP URL for the owner.
        /// </summary>
        string Url { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("RepositoryOwner")]
    internal class StubIRepositoryOwner : QueryableValue<StubIRepositoryOwner>, IRepositoryOwner
    {
        internal StubIRepositoryOwner(Expression expression) : base(expression)
        {
        }

        public string AvatarUrl(Arg<int>? size = null) => default;

        public ID Id { get; }

        public string Login { get; }

        public RepositoryConnection Repositories(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<RepositoryAffiliation?>>? affiliations = null, Arg<bool>? hasIssuesEnabled = null, Arg<bool>? isArchived = null, Arg<bool>? isFork = null, Arg<bool>? isLocked = null, Arg<RepositoryOrder>? orderBy = null, Arg<IEnumerable<RepositoryAffiliation?>>? ownerAffiliations = null, Arg<RepositoryPrivacy>? privacy = null, Arg<RepositoryVisibility>? visibility = null) => this.CreateMethodCall(x => x.Repositories(first, after, last, before, affiliations, hasIssuesEnabled, isArchived, isFork, isLocked, orderBy, ownerAffiliations, privacy, visibility), Octokit.GraphQL.Model.RepositoryConnection.Create);

        public Repository Repository(Arg<string> name, Arg<bool>? followRenames = null) => this.CreateMethodCall(x => x.Repository(name, followRenames), Octokit.GraphQL.Model.Repository.Create);

        public string ResourcePath { get; }

        public string Url { get; }

        internal static StubIRepositoryOwner Create(Expression expression)
        {
            return new StubIRepositoryOwner(expression);
        }
    }
}