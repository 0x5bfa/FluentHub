namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An entry in the audit log.
    /// </summary>
    [GraphQLIdentifier("AuditEntry")]
    public interface IAuditEntry : IQueryableValue<IAuditEntry>, IQueryableInterface
    {
        /// <summary>
        /// The action name
        /// </summary>
        string Action { get; }

        /// <summary>
        /// The user who initiated the action
        /// </summary>
        AuditEntryActor Actor { get; }

        /// <summary>
        /// The IP address of the actor
        /// </summary>
        string ActorIp { get; }

        /// <summary>
        /// A readable representation of the actor's location
        /// </summary>
        ActorLocation ActorLocation { get; }

        /// <summary>
        /// The username of the user who initiated the action
        /// </summary>
        string ActorLogin { get; }

        /// <summary>
        /// The HTTP path for the actor.
        /// </summary>
        string ActorResourcePath { get; }

        /// <summary>
        /// The HTTP URL for the actor.
        /// </summary>
        string ActorUrl { get; }

        /// <summary>
        /// The time the action was initiated
        /// </summary>
        string CreatedAt { get; }

        /// <summary>
        /// The corresponding operation type for the action
        /// </summary>
        OperationType? OperationType { get; }

        /// <summary>
        /// The user affected by the action
        /// </summary>
        User User { get; }

        /// <summary>
        /// For actions involving two users, the actor is the initiator and the user is the affected user.
        /// </summary>
        string UserLogin { get; }

        /// <summary>
        /// The HTTP path for the user.
        /// </summary>
        string UserResourcePath { get; }

        /// <summary>
        /// The HTTP URL for the user.
        /// </summary>
        string UserUrl { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("AuditEntry")]
    internal class StubIAuditEntry : QueryableValue<StubIAuditEntry>, IAuditEntry
    {
        internal StubIAuditEntry(Expression expression) : base(expression)
        {
        }

        public string Action { get; }

        public AuditEntryActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.AuditEntryActor.Create);

        public string ActorIp { get; }

        public ActorLocation ActorLocation => this.CreateProperty(x => x.ActorLocation, Octokit.GraphQL.Model.ActorLocation.Create);

        public string ActorLogin { get; }

        public string ActorResourcePath { get; }

        public string ActorUrl { get; }

        public string CreatedAt { get; }

        public OperationType? OperationType { get; }

        public User User => this.CreateProperty(x => x.User, Octokit.GraphQL.Model.User.Create);

        public string UserLogin { get; }

        public string UserResourcePath { get; }

        public string UserUrl { get; }

        internal static StubIAuditEntry Create(Expression expression)
        {
            return new StubIAuditEntry(expression);
        }
    }
}