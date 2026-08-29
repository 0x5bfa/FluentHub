namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A curatable list of repositories relating to a repository owner, which defaults to showing the most popular repositories they own.
    /// </summary>
    public class ProfileItemShowcase : QueryableValue<ProfileItemShowcase>
    {
        internal ProfileItemShowcase(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Whether or not the owner has pinned any repositories or gists.
        /// </summary>
        public bool HasPinnedItems { get; }

        /// <summary>
        /// The repositories and gists in the showcase. If the profile owner has any pinned items, those will be returned. Otherwise, the profile owner's popular repositories will be returned.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public PinnableItemConnection Items(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Items(first, after, last, before), Octokit.GraphQL.Model.PinnableItemConnection.Create);

        internal static ProfileItemShowcase Create(Expression expression)
        {
            return new ProfileItemShowcase(expression);
        }
    }
}