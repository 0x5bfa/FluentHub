namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An invitation for a user to be added to a repository.
    /// </summary>
    public class RepositoryInvitation : QueryableValue<RepositoryInvitation>
    {
        internal RepositoryInvitation(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The email address that received the invitation.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// The Node ID of the RepositoryInvitation object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The user who received the invitation.
        /// </summary>
        public User Invitee => this.CreateProperty(x => x.Invitee, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The user who created the invitation.
        /// </summary>
        public User Inviter => this.CreateProperty(x => x.Inviter, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The permalink for this repository invitation.
        /// </summary>
        public string Permalink { get; }

        /// <summary>
        /// The permission granted on this repository by this invitation.
        /// </summary>
        public RepositoryPermission Permission { get; }

        /// <summary>
        /// The Repository the user is invited to.
        /// </summary>
        public IRepositoryInfo Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Internal.StubIRepositoryInfo.Create);

        internal static RepositoryInvitation Create(Expression expression)
        {
            return new RepositoryInvitation(expression);
        }
    }
}