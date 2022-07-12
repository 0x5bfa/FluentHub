namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An email belonging to a user account on an Enterprise Server installation.
    /// </summary>
    public class EnterpriseServerUserAccountEmail
    {
        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The email address.
        /// </summary>
        public string Email { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Indicates whether this is the primary email of the associated user account.
        /// </summary>
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// The user account to which the email belongs.
        /// </summary>
        public EnterpriseServerUserAccount UserAccount { get; set; }
    }
}