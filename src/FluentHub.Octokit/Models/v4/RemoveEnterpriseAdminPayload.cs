// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated return type of RemoveEnterpriseAdmin
	/// </summary>
	public class RemoveEnterpriseAdminPayload
	{
		/// <summary>
		/// The user who was removed as an administrator.
		/// </summary>
		public User Admin { get; set; }

		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }

		/// <summary>
		/// The updated enterprise.
		/// </summary>
		public Enterprise Enterprise { get; set; }

		/// <summary>
		/// A message confirming the result of removing an administrator.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// The viewer performing the mutation.
		/// </summary>
		public User Viewer { get; set; }
	}
}
