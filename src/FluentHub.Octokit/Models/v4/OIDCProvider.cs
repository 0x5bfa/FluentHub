namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An OIDC identity provider configured to provision identities for an enterprise.
    /// </summary>
    public class OIDCProvider
    {
        /// <summary>
        /// The enterprise this identity provider belongs to.
        /// </summary>
        public Enterprise Enterprise { get; set; }

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
        public ExternalIdentityConnection ExternalIdentities { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The OIDC identity provider type
        /// </summary>
        public OIDCProviderType ProviderType { get; set; }

        /// <summary>
        /// The id of the tenant this provider is attached to
        /// </summary>
        public string TenantId { get; set; }
    }
}