namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An Enterprise Server installation.
    /// </summary>
    public class EnterpriseServerInstallation : QueryableValue<EnterpriseServerInstallation>
    {
        internal EnterpriseServerInstallation(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The customer name to which the Enterprise Server installation belongs.
        /// </summary>
        public string CustomerName { get; }

        /// <summary>
        /// The host name of the Enterprise Server installation.
        /// </summary>
        public string HostName { get; }

        /// <summary>
        /// The Node ID of the EnterpriseServerInstallation object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Whether or not the installation is connected to an Enterprise Server installation via GitHub Connect.
        /// </summary>
        public bool IsConnected { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// User accounts on this Enterprise Server installation.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for Enterprise Server user accounts returned from the connection.</param>
        public EnterpriseServerUserAccountConnection UserAccounts(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<EnterpriseServerUserAccountOrder>? orderBy = null) => this.CreateMethodCall(x => x.UserAccounts(first, after, last, before, orderBy), Octokit.GraphQL.Model.EnterpriseServerUserAccountConnection.Create);

        /// <summary>
        /// User accounts uploads for the Enterprise Server installation.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for Enterprise Server user accounts uploads returned from the connection.</param>
        public EnterpriseServerUserAccountsUploadConnection UserAccountsUploads(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<EnterpriseServerUserAccountsUploadOrder>? orderBy = null) => this.CreateMethodCall(x => x.UserAccountsUploads(first, after, last, before, orderBy), Octokit.GraphQL.Model.EnterpriseServerUserAccountsUploadConnection.Create);

        internal static EnterpriseServerInstallation Create(Expression expression)
        {
            return new EnterpriseServerInstallation(expression);
        }
    }
}