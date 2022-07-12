namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An external identity provisioned by SAML SSO or SCIM.
    /// </summary>
    public class ExternalIdentity
    {
        /// <summary>
        /// The GUID for this identity
        /// </summary>
        public string Guid { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Organization invitation for this SCIM-provisioned external identity
        /// </summary>
        public OrganizationInvitation OrganizationInvitation { get; set; }

        /// <summary>
        /// SAML Identity attributes
        /// </summary>
        public ExternalIdentitySamlAttributes SamlIdentity { get; set; }

        /// <summary>
        /// SCIM Identity attributes
        /// </summary>
        public ExternalIdentityScimAttributes ScimIdentity { get; set; }

        /// <summary>
        /// User linked to this external identity. Will be NULL if this identity has not been claimed by an organization member.
        /// </summary>
        public User User { get; set; }
    }
}