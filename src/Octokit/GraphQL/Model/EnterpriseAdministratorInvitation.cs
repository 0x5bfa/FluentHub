namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An invitation for a user to become an owner or billing manager of an enterprise.
    /// </summary>
    public class EnterpriseAdministratorInvitation : QueryableValue<EnterpriseAdministratorInvitation>
    {
        internal EnterpriseAdministratorInvitation(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The email of the person who was invited to the enterprise.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// The enterprise the invitation is for.
        /// </summary>
        public Enterprise Enterprise => this.CreateProperty(x => x.Enterprise, Octokit.GraphQL.Model.Enterprise.Create);

        /// <summary>
        /// The Node ID of the EnterpriseAdministratorInvitation object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The user who was invited to the enterprise.
        /// </summary>
        public User Invitee => this.CreateProperty(x => x.Invitee, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The user who created the invitation.
        /// </summary>
        public User Inviter => this.CreateProperty(x => x.Inviter, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The invitee's pending role in the enterprise (owner or billing_manager).
        /// </summary>
        public EnterpriseAdministratorRole Role { get; }

        internal static EnterpriseAdministratorInvitation Create(Expression expression)
        {
            return new EnterpriseAdministratorInvitation(expression);
        }
    }
}