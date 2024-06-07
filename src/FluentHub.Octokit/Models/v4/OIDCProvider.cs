// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An OIDC identity provider configured to provision identities for an enterprise. Visible to enterprise owners or enterprise owners' personal access tokens (classic) with read:enterprise or admin:enterprise scope.
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

		/// <summary>
		/// The Node ID of the OIDCProvider object
		/// </summary>
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
