namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An Invitation for a user to an organization.
    /// </summary>
    public class OrganizationInvitation
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
        /// The email address of the user invited to the organization.
        /// </summary>
        public string Email { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The source of the invitation.
        /// </summary>
        public OrganizationInvitationSource InvitationSource { get; set; }

        /// <summary>
        /// The type of invitation that was sent (e.g. email, user).
        /// </summary>
        public OrganizationInvitationType InvitationType { get; set; }

        /// <summary>
        /// The user who was invited to the organization.
        /// </summary>
        public User Invitee { get; set; }

        /// <summary>
        /// The user who created the invitation.
        /// </summary>
        public User Inviter { get; set; }

        /// <summary>
        /// The organization the invite is for
        /// </summary>
        public Organization Organization { get; set; }

        /// <summary>
        /// The user's pending role in the organization (e.g. member, owner).
        /// </summary>
        public OrganizationInvitationRole Role { get; set; }
    }
}