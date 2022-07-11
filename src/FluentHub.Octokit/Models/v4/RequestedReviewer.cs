namespace FluentHub.Octokit.v4.Model
{
    using System;

    /// <summary>
    /// Types that can be requested reviewers.
    /// </summary>
    public class RequestedReviewer
    {
        
            /// <summary>
            /// A placeholder user for attribution of imported data on GitHub.
            /// </summary>
        public Mannequin Mannequin { get; set; }

            /// <summary>
            /// A team of users in an organization.
            /// </summary>
        public Team Team { get; set; }

            /// <summary>
            /// A user is an individual's account on GitHub that owns repositories and can make new content.
            /// </summary>
        public User User { get; set; }
    }
}