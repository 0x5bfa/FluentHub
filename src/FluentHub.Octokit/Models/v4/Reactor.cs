namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Types that can be assigned to reactions.
    /// </summary>
    public class Reactor
    {
        /// <summary>
        /// A special type of user which takes actions on behalf of GitHub Apps.
        /// </summary>
        public Bot Bot { get; set; }

        /// <summary>
        /// A placeholder user for attribution of imported data on GitHub.
        /// </summary>
        public Mannequin Mannequin { get; set; }

        /// <summary>
        /// An account on GitHub, with one or more owners, that has repositories, members and teams.
        /// </summary>
        public Organization Organization { get; set; }

        /// <summary>
        /// A user is an individual's account on GitHub that owns repositories and can make new content.
        /// </summary>
        public User User { get; set; }
    }
}