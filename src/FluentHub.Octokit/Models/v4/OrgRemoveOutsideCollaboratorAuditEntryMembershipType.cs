// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The type of membership a user has with an Organization.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum OrgRemoveOutsideCollaboratorAuditEntryMembershipType
	{
		/// <summary>
		/// An outside collaborator is a person who isn't explicitly a member of the Organization, but who has Read, Write, or Admin permissions to one or more repositories in the organization.
		/// </summary>
		[EnumMember(Value = "OUTSIDE_COLLABORATOR")]
		OutsideCollaborator,

		/// <summary>
		/// An unaffiliated collaborator is a person who is not a member of the Organization and does not have access to any repositories in the organization.
		/// </summary>
		[EnumMember(Value = "UNAFFILIATED")]
		Unaffiliated,

		/// <summary>
		/// A billing manager is a user who manages the billing settings for the Organization, such as updating payment information.
		/// </summary>
		[EnumMember(Value = "BILLING_MANAGER")]
		BillingManager,
	}
}
