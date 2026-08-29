namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// SAML attributes for the External Identity
    /// </summary>
    public class ExternalIdentitySamlAttributes : QueryableValue<ExternalIdentitySamlAttributes>
    {
        internal ExternalIdentitySamlAttributes(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// SAML Identity attributes
        /// </summary>
        public IQueryableList<ExternalIdentityAttribute> Attributes => this.CreateProperty(x => x.Attributes);

        /// <summary>
        /// The emails associated with the SAML identity
        /// </summary>
        public IQueryableList<UserEmailMetadata> Emails => this.CreateProperty(x => x.Emails);

        /// <summary>
        /// Family name of the SAML identity
        /// </summary>
        public string FamilyName { get; }

        /// <summary>
        /// Given name of the SAML identity
        /// </summary>
        public string GivenName { get; }

        /// <summary>
        /// The groups linked to this identity in IDP
        /// </summary>
        public IEnumerable<string> Groups { get; }

        /// <summary>
        /// The NameID of the SAML identity
        /// </summary>
        public string NameId { get; }

        /// <summary>
        /// The userName of the SAML identity
        /// </summary>
        public string Username { get; }

        internal static ExternalIdentitySamlAttributes Create(Expression expression)
        {
            return new ExternalIdentitySamlAttributes(expression);
        }
    }
}