namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An OIDC identity provider configured to provision identities for an enterprise. Visible to enterprise owners or enterprise owners' personal access tokens (classic) with read:enterprise or admin:enterprise scope.
    /// </summary>
    public class OIDCProvider : QueryableValue<OIDCProvider>
    {
        internal OIDCProvider(Expression expression) : base(expression)
        {
        }

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
        /// The Node ID of the OIDCProvider object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The OIDC identity provider type
        /// </summary>
        public OIDCProviderType ProviderType { get; }

        /// <summary>
        /// The id of the tenant this provider is attached to
        /// </summary>
        public string TenantId { get; }

        internal static OIDCProvider Create(Expression expression)
        {
            return new OIDCProvider(expression);
        }
    }
}