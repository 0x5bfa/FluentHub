namespace FluentHub.Octokit.v4.Model
{
    using System;

    /// <summary>
    /// Types that can be an actor.
    /// </summary>
    public class PushAllowanceActor
    {
        
            /// <summary>
            /// A GitHub App.
            /// </summary>
        public App App { get; set; }

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