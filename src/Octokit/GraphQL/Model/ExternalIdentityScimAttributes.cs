namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// SCIM attributes for the External Identity
    /// </summary>
    public class ExternalIdentityScimAttributes : QueryableValue<ExternalIdentityScimAttributes>
    {
        internal ExternalIdentityScimAttributes(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The emails associated with the SCIM identity
        /// </summary>
        public IQueryableList<UserEmailMetadata> Emails => this.CreateProperty(x => x.Emails);

        /// <summary>
        /// Family name of the SCIM identity
        /// </summary>
        public string FamilyName { get; }

        /// <summary>
        /// Given name of the SCIM identity
        /// </summary>
        public string GivenName { get; }

        /// <summary>
        /// The groups linked to this identity in IDP
        /// </summary>
        public IEnumerable<string> Groups { get; }

        /// <summary>
        /// The userName of the SCIM identity
        /// </summary>
        public string Username { get; }

        internal static ExternalIdentityScimAttributes Create(Expression expression)
        {
            return new ExternalIdentityScimAttributes(expression);
        }
    }
}