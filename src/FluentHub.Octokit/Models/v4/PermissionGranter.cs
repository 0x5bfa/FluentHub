namespace FluentHub.Octokit.v4.Model
{
    using System;

    /// <summary>
    /// Types that can grant permissions on a repository to a user
    /// </summary>
    public class PermissionGranter
    {
        
            /// <summary>
            /// An account on GitHub, with one or more owners, that has repositories, members and teams.
            /// </summary>
        public Organization Organization { get; set; }

            /// <summary>
            /// A repository contains the content for a project.
            /// </summary>
        public Repository Repository { get; set; }

            /// <summary>
            /// A team of users in an organization.
            /// </summary>
        public Team Team { get; set; }
    }
}