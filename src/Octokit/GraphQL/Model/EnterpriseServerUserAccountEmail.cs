namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An email belonging to a user account on an Enterprise Server installation.
    /// </summary>
    public class EnterpriseServerUserAccountEmail : QueryableValue<EnterpriseServerUserAccountEmail>
    {
        internal EnterpriseServerUserAccountEmail(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The email address.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// The Node ID of the EnterpriseServerUserAccountEmail object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Indicates whether this is the primary email of the associated user account.
        /// </summary>
        public bool IsPrimary { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The user account to which the email belongs.
        /// </summary>
        public EnterpriseServerUserAccount UserAccount => this.CreateProperty(x => x.UserAccount, Octokit.GraphQL.Model.EnterpriseServerUserAccount.Create);

        internal static EnterpriseServerUserAccountEmail Create(Expression expression)
        {
            return new EnterpriseServerUserAccountEmail(expression);
        }
    }
}