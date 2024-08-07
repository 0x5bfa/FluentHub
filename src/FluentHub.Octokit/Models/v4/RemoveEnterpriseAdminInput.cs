// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated input type of RemoveEnterpriseAdmin
	/// </summary>
	public class RemoveEnterpriseAdminInput
	{
		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }

		/// <summary>
		/// The Enterprise ID from which to remove the administrator.
		/// </summary>
		public ID EnterpriseId { get; set; }

		/// <summary>
		/// The login of the user to remove as an administrator.
		/// </summary>
		public string Login { get; set; }
	}
}
