namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// An entry in the audit log.
    /// </summary>
    public interface IAuditEntry
    {        /// <summary>
        /// The action name
        /// </summary>
        string Action { get; set; }

        /// <summary>
        /// The user who initiated the action
        /// </summary>
        AuditEntryActor Actor { get; set; }

        /// <summary>
        /// The IP address of the actor
        /// </summary>
        string ActorIp { get; set; }

        /// <summary>
        /// A readable representation of the actor's location
        /// </summary>
        ActorLocation ActorLocation { get; set; }

        /// <summary>
        /// The username of the user who initiated the action
        /// </summary>
        string ActorLogin { get; set; }

        /// <summary>
        /// The HTTP path for the actor.
        /// </summary>
        string ActorResourcePath { get; set; }

        /// <summary>
        /// The HTTP URL for the actor.
        /// </summary>
        string ActorUrl { get; set; }

        /// <summary>
        /// The time the action was initiated
        /// </summary>
        string CreatedAt { get; set; }

        /// <summary>
        /// The corresponding operation type for the action
        /// </summary>
        OperationType? OperationType { get; set; }

        /// <summary>
        /// The user affected by the action
        /// </summary>
        User User { get; set; }

        /// <summary>
        /// For actions involving two users, the actor is the initiator and the user is the affected user.
        /// </summary>
        string UserLogin { get; set; }

        /// <summary>
        /// The HTTP path for the user.
        /// </summary>
        string UserResourcePath { get; set; }

        /// <summary>
        /// The HTTP URL for the user.
        /// </summary>
        string UserUrl { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    internal class AuditEntry : IAuditEntry
    {
        public string Action { get; set; }

        public AuditEntryActor Actor { get; set; }

        public string ActorIp { get; set; }

        public ActorLocation ActorLocation { get; set; }

        public string ActorLogin { get; set; }

        public string ActorResourcePath { get; set; }

        public string ActorUrl { get; set; }

        public string CreatedAt { get; set; }

        public OperationType? OperationType { get; set; }

        public User User { get; set; }

        public string UserLogin { get; set; }

        public string UserResourcePath { get; set; }

        public string UserUrl { get; set; }
    }
}