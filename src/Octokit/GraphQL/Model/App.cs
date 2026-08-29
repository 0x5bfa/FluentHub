namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A GitHub App.
    /// </summary>
    public class App : QueryableValue<App>
    {
        internal App(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The description of the app.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The Node ID of the App object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The IP addresses of the app.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for IP allow list entries returned.</param>
        public IpAllowListEntryConnection IpAllowListEntries(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IpAllowListEntryOrder>? orderBy = null) => this.CreateMethodCall(x => x.IpAllowListEntries(first, after, last, before, orderBy), Octokit.GraphQL.Model.IpAllowListEntryConnection.Create);

        /// <summary>
        /// The hex color code, without the leading '#', for the logo background.
        /// </summary>
        public string LogoBackgroundColor { get; }

        /// <summary>
        /// A URL pointing to the app's logo.
        /// </summary>
        /// <param name="size">The size of the resulting image.</param>
        public string LogoUrl(Arg<int>? size = null) => default;

        /// <summary>
        /// The name of the app.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A slug based on the name of the app for use in URLs.
        /// </summary>
        public string Slug { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The URL to the app's homepage.
        /// </summary>
        public string Url { get; }

        internal static App Create(Expression expression)
        {
            return new App(expression);
        }
    }
}