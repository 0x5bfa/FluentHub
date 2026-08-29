namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An identity provider configured to provision identities for an enterprise. Visible to enterprise owners or enterprise owners' personal access tokens (classic) with read:enterprise or admin:enterprise scope.
    /// </summary>
    public class EnterpriseIdentityProvider : QueryableValue<EnterpriseIdentityProvider>
    {
        internal EnterpriseIdentityProvider(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The digest algorithm used to sign SAML requests for the identity provider.
        /// </summary>
        public SamlDigestAlgorithm? DigestMethod { get; }

        /// <summary>
        /// The enterprise this identity provider belongs to.
        /// </summary>
        public Enterprise Enterprise => this.CreateProperty(x => x.Enterprise, Octokit.GraphQL.Model.Enterprise.Create);

        /// <summary>
        /// ExternalIdentities provisioned by this identity provider.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="login">Filter to external identities with the users login</param>
        /// <param name="membersOnly">Filter to external identities with valid org membership only</param>
        /// <param name="userName">Filter to external identities with the users userName/NameID attribute</param>
        public ExternalIdentityConnection ExternalIdentities(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<string>? login = null, Arg<bool>? membersOnly = null, Arg<string>? userName = null) => this.CreateMethodCall(x => x.ExternalIdentities(first, after, last, before, login, membersOnly, userName), Octokit.GraphQL.Model.ExternalIdentityConnection.Create);

        /// <summary>
        /// The Node ID of the EnterpriseIdentityProvider object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The x509 certificate used by the identity provider to sign assertions and responses.
        /// </summary>
        public string IdpCertificate { get; }

        /// <summary>
        /// The Issuer Entity ID for the SAML identity provider.
        /// </summary>
        public string Issuer { get; }

        /// <summary>
        /// Recovery codes that can be used by admins to access the enterprise if the identity provider is unavailable.
        /// </summary>
        public IEnumerable<string> RecoveryCodes { get; }

        /// <summary>
        /// The signature algorithm used to sign SAML requests for the identity provider.
        /// </summary>
        public SamlSignatureAlgorithm? SignatureMethod { get; }

        /// <summary>
        /// The URL endpoint for the identity provider's SAML SSO.
        /// </summary>
        public string SsoUrl { get; }

        internal static EnterpriseIdentityProvider Create(Expression expression)
        {
            return new EnterpriseIdentityProvider(expression);
        }
    }
}