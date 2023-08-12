// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An invitation for a user to be added to a repository.
	/// </summary>
	public class RepositoryInvitation
	{
		/// <summary>
		/// The email address that received the invitation.
		/// </summary>
		public string Email { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// The user who received the invitation.
		/// </summary>
		public User Invitee { get; set; }

		/// <summary>
		/// The user who created the invitation.
		/// </summary>
		public User Inviter { get; set; }

		/// <summary>
		/// The permalink for this repository invitation.
		/// </summary>
		public string Permalink { get; set; }

		/// <summary>
		/// The permission granted on this repository by this invitation.
		/// </summary>
		public RepositoryPermission Permission { get; set; }

		/// <summary>
		/// The Repository the user is invited to.
		/// </summary>
		public IRepositoryInfo Repository { get; set; }
	}
}
