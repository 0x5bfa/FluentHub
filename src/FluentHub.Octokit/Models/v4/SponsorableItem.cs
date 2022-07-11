namespace FluentHub.Octokit.Models.v4
{
    using System;

    /// <summary>
    /// Entities that can be sponsored via GitHub Sponsors
    /// </summary>
public class SponsorableItem
    {
        
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