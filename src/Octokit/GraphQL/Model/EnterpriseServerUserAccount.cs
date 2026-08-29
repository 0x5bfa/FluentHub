namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A user account on an Enterprise Server installation.
    /// </summary>
    public class EnterpriseServerUserAccount : QueryableValue<EnterpriseServerUserAccount>
    {
        internal EnterpriseServerUserAccount(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// User emails belonging to this user account.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for Enterprise Server user account emails returned from the connection.</param>
        public EnterpriseServerUserAccountEmailConnection Emails(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<EnterpriseServerUserAccountEmailOrder>? orderBy = null) => this.CreateMethodCall(x => x.Emails(first, after, last, before, orderBy), Octokit.GraphQL.Model.EnterpriseServerUserAccountEmailConnection.Create);

        /// <summary>
        /// The Enterprise Server installation on which this user account exists.
        /// </summary>
        public EnterpriseServerInstallation EnterpriseServerInstallation => this.CreateProperty(x => x.EnterpriseServerInstallation, Octokit.GraphQL.Model.EnterpriseServerInstallation.Create);

        /// <summary>
        /// The Node ID of the EnterpriseServerUserAccount object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Whether the user account is a site administrator on the Enterprise Server installation.
        /// </summary>
        public bool IsSiteAdmin { get; }

        /// <summary>
        /// The login of the user account on the Enterprise Server installation.
        /// </summary>
        public string Login { get; }

        /// <summary>
        /// The profile name of the user account on the Enterprise Server installation.
        /// </summary>
        public string ProfileName { get; }

        /// <summary>
        /// The date and time when the user account was created on the Enterprise Server installation.
        /// </summary>
        public DateTimeOffset RemoteCreatedAt { get; }

        /// <summary>
        /// The ID of the user account on the Enterprise Server installation.
        /// </summary>
        public int RemoteUserId { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static EnterpriseServerUserAccount Create(Expression expression)
        {
            return new EnterpriseServerUserAccount(expression);
        }
    }
}