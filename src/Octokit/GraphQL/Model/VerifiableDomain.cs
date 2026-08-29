namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A domain that can be verified or approved for an organization or an enterprise.
    /// </summary>
    public class VerifiableDomain : QueryableValue<VerifiableDomain>
    {
        internal VerifiableDomain(Expression expression) : base(expression)
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
        /// The DNS host name that should be used for verification.
        /// </summary>
        public string DnsHostName { get; }

        /// <summary>
        /// The unicode encoded domain.
        /// </summary>
        public string Domain { get; }

        /// <summary>
        /// Whether a TXT record for verification with the expected host name was found.
        /// </summary>
        public bool HasFoundHostName { get; }

        /// <summary>
        /// Whether a TXT record for verification with the expected verification token was found.
        /// </summary>
        public bool HasFoundVerificationToken { get; }

        /// <summary>
        /// The Node ID of the VerifiableDomain object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Whether or not the domain is approved.
        /// </summary>
        public bool IsApproved { get; }

        /// <summary>
        /// Whether this domain is required to exist for an organization or enterprise policy to be enforced.
        /// </summary>
        public bool IsRequiredForPolicyEnforcement { get; }

        /// <summary>
        /// Whether or not the domain is verified.
        /// </summary>
        public bool IsVerified { get; }

        /// <summary>
        /// The owner of the domain.
        /// </summary>
        public VerifiableDomainOwner Owner => this.CreateProperty(x => x.Owner, Octokit.GraphQL.Model.VerifiableDomainOwner.Create);

        /// <summary>
        /// The punycode encoded domain.
        /// </summary>
        public string PunycodeEncodedDomain { get; }

        /// <summary>
        /// The time that the current verification token will expire.
        /// </summary>
        public DateTimeOffset? TokenExpirationTime { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The current verification token for the domain.
        /// </summary>
        public string VerificationToken { get; }

        internal static VerifiableDomain Create(Expression expression)
        {
            return new VerifiableDomain(expression);
        }
    }
}