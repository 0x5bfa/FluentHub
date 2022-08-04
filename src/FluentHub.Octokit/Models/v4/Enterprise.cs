namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An account to manage multiple organizations with consolidated policy and billing.
    /// </summary>
    public class Enterprise
    {
        /// <summary>
        /// A URL pointing to the enterprise's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// Enterprise billing information visible to enterprise billing managers.
        /// </summary>
        public EnterpriseBillingInfo BillingInfo { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was created."
        /// <summary>
        public string CreatedAtHumanized { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; set; }

        /// <summary>
        /// The description of the enterprise.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The description of the enterprise as HTML.
        /// </summary>
        public string DescriptionHTML { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The location of the enterprise.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// A list of users who are members of this enterprise.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="deployment">Only return members within the selected GitHub Enterprise deployment</param>
        /// <param name="orderBy">Ordering options for members returned from the connection.</param>
        /// <param name="organizationLogins">Only return members within the organizations with these logins</param>
        /// <param name="query">The search string to look for.</param>
        /// <param name="role">The role of the user in the enterprise organization or server.</param>
        public EnterpriseMemberConnection Members { get; set; }

        /// <summary>
        /// The name of the enterprise.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A list of organizations that belong to this enterprise.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for organizations returned from the connection.</param>
        /// <param name="query">The search string to look for.</param>
        /// <param name="viewerOrganizationRole">The viewer's role in an organization.</param>
        public OrganizationConnection Organizations { get; set; }

        /// <summary>
        /// Enterprise information only visible to enterprise owners.
        /// </summary>
        public EnterpriseOwnerInfo OwnerInfo { get; set; }

        /// <summary>
        /// The HTTP path for this enterprise.
        /// </summary>
        public string ResourcePath { get; set; }

        /// <summary>
        /// The URL-friendly identifier for the enterprise.
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// The HTTP URL for this enterprise.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Is the current viewer an admin of this enterprise?
        /// </summary>
        public bool ViewerIsAdmin { get; set; }

        /// <summary>
        /// The URL of the enterprise website.
        /// </summary>
        public string WebsiteUrl { get; set; }
    }
}