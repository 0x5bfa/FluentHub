namespace FluentHub.Octokit.v4.Model
{
    using System;

    /// <summary>
    /// Types that can initiate an audit log event.
    /// </summary>
    public class AuditEntryActor
    {
        
            /// <summary>
            /// A special type of user which takes actions on behalf of GitHub Apps.
            /// </summary>
        public Bot Bot { get; set; }

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