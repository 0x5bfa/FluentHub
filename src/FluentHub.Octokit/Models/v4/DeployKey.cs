namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A repository deploy key.
    /// </summary>
    public class DeployKey
    {
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
        /// The deploy key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Whether or not the deploy key is read only.
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// The deploy key title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Whether or not the deploy key has been verified.
        /// </summary>
        public bool Verified { get; set; }
    }
}