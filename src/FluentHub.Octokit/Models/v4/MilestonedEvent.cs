namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a 'milestoned' event on a given issue or pull request.
    /// </summary>
    public class MilestonedEvent
    {
        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was created."
        /// <summary>
        public string CreatedAtHumanized { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Identifies the milestone title associated with the 'milestoned' event.
        /// </summary>
        public string MilestoneTitle { get; set; }

        /// <summary>
        /// Object referenced by event.
        /// </summary>
        public MilestoneItem Subject { get; set; }
    }
}