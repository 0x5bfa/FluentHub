namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An Identity Provider configured to provision SAML and SCIM identities for Organizations. Visible to (1) organization owners, (2) organization owners' personal access tokens (classic) with read:org or admin:org scope, (3) GitHub App with an installation token with read or write access to members.
    /// </summary>
    public class OrganizationIdentityProvider : QueryableValue<OrganizationIdentityProvider>
    {
        internal OrganizationIdentityProvider(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The digest algorithm used to sign SAML requests for the Identity Provider.
        /// </summary>
        public string DigestMethod { get; }

        /// <summary>
        /// External Identities provisioned by this Identity Provider
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
        /// The Node ID of the OrganizationIdentityProvider object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The x509 certificate used by the Identity Provider to sign assertions and responses.
        /// </summary>
        public string IdpCertificate { get; }

        /// <summary>
        /// The Issuer Entity ID for the SAML Identity Provider
        /// </summary>
        public string Issuer { get; }

        /// <summary>
        /// Organization this Identity Provider belongs to
        /// </summary>
        public Organization Organization => this.CreateProperty(x => x.Organization, Octokit.GraphQL.Model.Organization.Create);

        /// <summary>
        /// The signature algorithm used to sign SAML requests for the Identity Provider.
        /// </summary>
        public string SignatureMethod { get; }

        /// <summary>
        /// The URL endpoint for the Identity Provider's SAML SSO.
        /// </summary>
        public string SsoUrl { get; }

        internal static OrganizationIdentityProvider Create(Expression expression)
        {
            return new OrganizationIdentityProvider(expression);
        }
    }
}