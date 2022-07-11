namespace FluentHub.Octokit.v4.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents any entity on GitHub that has a profile page.
    /// </summary>
    public interface IProfileOwner
    {
        /// <summary>
        /// Determine if this repository owner has any items that can be pinned to their profile.
        /// </summary>
        /// <param name="type">Filter to only a particular kind of pinnable item.</param>
        bool AnyPinnableItems { get; set; }

        /// <summary>
        /// The public profile email.
        /// </summary>
        string Email { get; set; }

        ID Id { get; set; }

        /// <summary>
        /// Showcases a selection of repositories and gists that the profile owner has either curated or that have been selected automatically based on popularity.
        /// </summary>
        ProfileItemShowcase ItemShowcase { get; set; }

        /// <summary>
        /// The public profile location.
        /// </summary>
        string Location { get; set; }

        /// <summary>
        /// The username used to login.
        /// </summary>
        string Login { get; set; }

        /// <summary>
        /// The public profile name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// A list of repositories and gists this profile owner can pin to their profile.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="types">Filter the types of pinnable items that are returned.</param>
        PinnableItemConnection PinnableItems { get; set; }

        /// <summary>
        /// A list of repositories and gists this profile owner has pinned to their profile
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="types">Filter the types of pinned items that are returned.</param>
        PinnableItemConnection PinnedItems { get; set; }

        /// <summary>
        /// Returns how many more items this profile owner can pin to their profile.
        /// </summary>
        int PinnedItemsRemaining { get; set; }

        /// <summary>
        /// Can the viewer pin repositories and gists to the profile?
        /// </summary>
        bool ViewerCanChangePinnedItems { get; set; }

        /// <summary>
        /// The public profile website URL.
        /// </summary>
        string WebsiteUrl { get; set; }
    }
}

namespace FluentHub.Octokit.v4.Model.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubIProfileOwner
    {
        

        public bool AnyPinnableItems { get; set; }

        public string Email { get; set; }

        public ID Id { get; set; }

        public ProfileItemShowcase ItemShowcase { get; set; }

        public string Location { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }

        public PinnableItemConnection PinnableItems { get; set; }

        public PinnableItemConnection PinnedItems { get; set; }

        public int PinnedItemsRemaining { get; set; }

        public bool ViewerCanChangePinnedItems { get; set; }

        public string WebsiteUrl { get; set; }
    }
}