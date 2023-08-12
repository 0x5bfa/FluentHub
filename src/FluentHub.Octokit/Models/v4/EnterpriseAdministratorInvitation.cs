// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An invitation for a user to become an owner or billing manager of an enterprise.
	/// </summary>
	public class EnterpriseAdministratorInvitation
	{
		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The email of the person who was invited to the enterprise.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// The enterprise the invitation is for.
		/// </summary>
		public Enterprise Enterprise { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// The user who was invited to the enterprise.
		/// </summary>
		public User Invitee { get; set; }

		/// <summary>
		/// The user who created the invitation.
		/// </summary>
		public User Inviter { get; set; }

		/// <summary>
		/// The invitee's pending role in the enterprise (owner or billing_manager).
		/// </summary>
		public EnterpriseAdministratorRole Role { get; set; }
	}
}
