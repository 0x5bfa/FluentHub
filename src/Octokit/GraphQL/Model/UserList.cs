namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A user-curated list of repositories
    /// </summary>
    public class UserList : QueryableValue<UserList>
    {
        internal UserList(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The description of this list
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The Node ID of the UserList object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Whether or not this list is private
        /// </summary>
        public bool IsPrivate { get; }

        /// <summary>
        /// The items associated with this list
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserListItemsConnection Items(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Items(first, after, last, before), Octokit.GraphQL.Model.UserListItemsConnection.Create);

        /// <summary>
        /// The date and time at which this list was created or last had items added to it
        /// </summary>
        public DateTimeOffset LastAddedAt { get; }

        /// <summary>
        /// The name of this list
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The slug of this list
        /// </summary>
        public string Slug { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The user to which this list belongs
        /// </summary>
        public User User => this.CreateProperty(x => x.User, Octokit.GraphQL.Model.User.Create);

        internal static UserList Create(Expression expression)
        {
            return new UserList(expression);
        }
    }
}