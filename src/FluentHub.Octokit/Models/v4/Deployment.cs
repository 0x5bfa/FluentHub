namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents triggered deployment instance.
    /// </summary>
    public class Deployment
    {
        /// <summary>
        /// Identifies the commit sha of the deployment.
        /// </summary>
        public Commit Commit { get; set; }

        /// <summary>
        /// Identifies the oid of the deployment commit, even if the commit has been deleted.
        /// </summary>
        public string CommitOid { get; set; }

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
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; set; }

        /// <summary>
        /// The deployment description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The latest environment to which this deployment was made.
        /// </summary>
        public string Environment { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The latest environment to which this deployment was made.
        /// </summary>
        public string LatestEnvironment { get; set; }

        /// <summary>
        /// The latest status of this deployment.
        /// </summary>
        public DeploymentStatus LatestStatus { get; set; }

        /// <summary>
        /// The original environment to which this deployment was made.
        /// </summary>
        public string OriginalEnvironment { get; set; }

        /// <summary>
        /// Extra information that a deployment system might need.
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// Identifies the Ref of the deployment, if the deployment was created by ref.
        /// </summary>
        public Ref Ref { get; set; }

        /// <summary>
        /// Identifies the repository associated with the deployment.
        /// </summary>
        public Repository Repository { get; set; }

        /// <summary>
        /// The current state of the deployment.
        /// </summary>
        public DeploymentState? State { get; set; }

        /// <summary>
        /// A list of statuses associated with the deployment.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public DeploymentStatusConnection Statuses { get; set; }

        /// <summary>
        /// The deployment task.
        /// </summary>
        public string Task { get; set; }

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