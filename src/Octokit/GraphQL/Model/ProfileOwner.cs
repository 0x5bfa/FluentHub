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
    /// Represents any entity on GitHub that has a profile page.
    /// </summary>
    [GraphQLIdentifier("ProfileOwner")]
    public interface IProfileOwner : IQueryableValue<IProfileOwner>, IQueryableInterface
    {
        /// <summary>
        /// Determine if this repository owner has any items that can be pinned to their profile.
        /// </summary>
        /// <param name="type">Filter to only a particular kind of pinnable item.</param>
        bool AnyPinnableItems(Arg<PinnableItemType>? type = null);

        /// <summary>
        /// The public profile email.
        /// </summary>
        string Email { get; }

        /// <summary>
        /// The Node ID of the ProfileOwner object
        /// </summary>
        ID Id { get; }

        /// <summary>
        /// Showcases a selection of repositories and gists that the profile owner has either curated or that have been selected automatically based on popularity.
        /// </summary>
        ProfileItemShowcase ItemShowcase { get; }

        /// <summary>
        /// The public profile location.
        /// </summary>
        string Location { get; }

        /// <summary>
        /// The username used to login.
        /// </summary>
        string Login { get; }

        /// <summary>
        /// The public profile name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// A list of repositories and gists this profile owner can pin to their profile.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="types">Filter the types of pinnable items that are returned.</param>
        PinnableItemConnection PinnableItems(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<PinnableItemType>>? types = null);

        /// <summary>
        /// A list of repositories and gists this profile owner has pinned to their profile
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="types">Filter the types of pinned items that are returned.</param>
        PinnableItemConnection PinnedItems(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<PinnableItemType>>? types = null);

        /// <summary>
        /// Returns how many more items this profile owner can pin to their profile.
        /// </summary>
        int PinnedItemsRemaining { get; }

        /// <summary>
        /// Can the viewer pin repositories and gists to the profile?
        /// </summary>
        bool ViewerCanChangePinnedItems { get; }

        /// <summary>
        /// The public profile website URL.
        /// </summary>
        string WebsiteUrl { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("ProfileOwner")]
    internal class StubIProfileOwner : QueryableValue<StubIProfileOwner>, IProfileOwner
    {
        internal StubIProfileOwner(Expression expression) : base(expression)
        {
        }

        public bool AnyPinnableItems(Arg<PinnableItemType>? type = null) => default;

        public string Email { get; }

        public ID Id { get; }

        public ProfileItemShowcase ItemShowcase => this.CreateProperty(x => x.ItemShowcase, Octokit.GraphQL.Model.ProfileItemShowcase.Create);

        public string Location { get; }

        public string Login { get; }

        public string Name { get; }

        public PinnableItemConnection PinnableItems(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<PinnableItemType>>? types = null) => this.CreateMethodCall(x => x.PinnableItems(first, after, last, before, types), Octokit.GraphQL.Model.PinnableItemConnection.Create);

        public PinnableItemConnection PinnedItems(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<PinnableItemType>>? types = null) => this.CreateMethodCall(x => x.PinnedItems(first, after, last, before, types), Octokit.GraphQL.Model.PinnableItemConnection.Create);

        public int PinnedItemsRemaining { get; }

        public bool ViewerCanChangePinnedItems { get; }

        public string WebsiteUrl { get; }

        internal static StubIProfileOwner Create(Expression expression)
        {
            return new StubIProfileOwner(expression);
        }
    }
}