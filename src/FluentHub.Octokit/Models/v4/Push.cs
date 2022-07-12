namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A Git push.
    /// </summary>
    public class Push
    {
        public ID Id { get; set; }

        /// <summary>
        /// The SHA after the push
        /// </summary>
        public string NextSha { get; set; }

        /// <summary>
        /// The permalink for this push.
        /// </summary>
        public string Permalink { get; set; }

        /// <summary>
        /// The SHA before the push
        /// </summary>
        public string PreviousSha { get; set; }

        /// <summary>
        /// The actor who pushed
        /// </summary>
        public IActor Pusher { get; set; }

        /// <summary>
        /// The repository that was pushed to
        /// </summary>
        public Repository Repository { get; set; }
    }
}