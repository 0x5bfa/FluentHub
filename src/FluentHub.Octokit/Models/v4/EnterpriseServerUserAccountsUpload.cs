namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A user accounts upload from an Enterprise Server installation.
    /// </summary>
    public class EnterpriseServerUserAccountsUpload
    {
        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was created."
        /// <summary>
        public string CreatedAtHumanized { get; set; }

        /// <summary>
        /// The enterprise to which this upload belongs.
        /// </summary>
        public Enterprise Enterprise { get; set; }

        /// <summary>
        /// The Enterprise Server installation for which this upload was generated.
        /// </summary>
        public EnterpriseServerInstallation EnterpriseServerInstallation { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The name of the file uploaded.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The synchronization state of the upload
        /// </summary>
        public EnterpriseServerUserAccountsUploadSyncState SyncState { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was last updated."
        /// <summary>
        public string UpdatedAtHumanized { get; set; }
    }
}