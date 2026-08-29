namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A user accounts upload from an Enterprise Server installation.
    /// </summary>
    public class EnterpriseServerUserAccountsUpload : QueryableValue<EnterpriseServerUserAccountsUpload>
    {
        internal EnterpriseServerUserAccountsUpload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The enterprise to which this upload belongs.
        /// </summary>
        public Enterprise Enterprise => this.CreateProperty(x => x.Enterprise, Octokit.GraphQL.Model.Enterprise.Create);

        /// <summary>
        /// The Enterprise Server installation for which this upload was generated.
        /// </summary>
        public EnterpriseServerInstallation EnterpriseServerInstallation => this.CreateProperty(x => x.EnterpriseServerInstallation, Octokit.GraphQL.Model.EnterpriseServerInstallation.Create);

        /// <summary>
        /// The Node ID of the EnterpriseServerUserAccountsUpload object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The name of the file uploaded.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The synchronization state of the upload
        /// </summary>
        public EnterpriseServerUserAccountsUploadSyncState SyncState { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static EnterpriseServerUserAccountsUpload Create(Expression expression)
        {
            return new EnterpriseServerUserAccountsUpload(expression);
        }
    }
}