namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An account for a user who is an admin of an enterprise or a member of an enterprise through one or more organizations.
    /// </summary>
    public class EnterpriseUserAccount : QueryableValue<EnterpriseUserAccount>
    {
        internal EnterpriseUserAccount(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A URL pointing to the enterprise user account's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        public string AvatarUrl(Arg<int>? size = null) => default;

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The enterprise in which this user account exists.
        /// </summary>
        public Enterprise Enterprise => this.CreateProperty(x => x.Enterprise, Octokit.GraphQL.Model.Enterprise.Create);

        /// <summary>
        /// A list of Enterprise Server installations this user is a member of.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for installations returned from the connection.</param>
        /// <param name="query">The search string to look for.</param>
        /// <param name="role">The role of the user in the installation.</param>
        public EnterpriseServerInstallationMembershipConnection EnterpriseInstallations(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<EnterpriseServerInstallationOrder>? orderBy = null, Arg<string>? query = null, Arg<EnterpriseUserAccountMembershipRole>? role = null) => this.CreateMethodCall(x => x.EnterpriseInstallations(first, after, last, before, orderBy, query, role), Octokit.GraphQL.Model.EnterpriseServerInstallationMembershipConnection.Create);

        /// <summary>
        /// The Node ID of the EnterpriseUserAccount object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// An identifier for the enterprise user account, a login or email address
        /// </summary>
        public string Login { get; }

        /// <summary>
        /// The name of the enterprise user account
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A list of enterprise organizations this user is a member of.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for organizations returned from the connection.</param>
        /// <param name="query">The search string to look for.</param>
        /// <param name="role">The role of the user in the enterprise organization.</param>
        public EnterpriseOrganizationMembershipConnection Organizations(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrganizationOrder>? orderBy = null, Arg<string>? query = null, Arg<EnterpriseUserAccountMembershipRole>? role = null) => this.CreateMethodCall(x => x.Organizations(first, after, last, before, orderBy, query, role), Octokit.GraphQL.Model.EnterpriseOrganizationMembershipConnection.Create);

        /// <summary>
        /// The HTTP path for this user.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this user.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// The user within the enterprise.
        /// </summary>
        public User User => this.CreateProperty(x => x.User, Octokit.GraphQL.Model.User.Create);

        internal static EnterpriseUserAccount Create(Expression expression)
        {
            return new EnterpriseUserAccount(expression);
        }
    }
}