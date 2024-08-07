// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated return type of AddEnterpriseOrganizationMember
	/// </summary>
	public class AddEnterpriseOrganizationMemberPayload
	{
		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }

		/// <summary>
		/// The users who were added to the organization.
		/// </summary>
		public List<User> Users { get; set; }
	}
}
