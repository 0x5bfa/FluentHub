namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Describes the status of a given deployment attempt.
    /// </summary>
    public class DeploymentStatus
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
        /// Identifies the actor who triggered the deployment.
        /// </summary>
        public IActor Creator { get; set; }

        /// <summary>
        /// Identifies the deployment associated with status.
        /// </summary>
        public Deployment Deployment { get; set; }

        /// <summary>
        /// Identifies the description of the deployment.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Identifies the environment URL of the deployment.
        /// </summary>
        public string EnvironmentUrl { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Identifies the log URL of the deployment.
        /// </summary>
        public string LogUrl { get; set; }

        /// <summary>
        /// Identifies the current state of the deployment.
        /// </summary>
        public DeploymentStatusState State { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was last updated."
        /// <summary>
        public string UpdatedAtHumanized { get; set; }
    }
}