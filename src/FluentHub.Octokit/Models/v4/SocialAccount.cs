namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Social media profile associated with a user.
    /// </summary>
    public class SocialAccount
    {
        /// <summary>
        /// Name of the social media account as it appears on the profile.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Software or company that hosts the social media account.
        /// </summary>
        public SocialAccountProvider Provider { get; set; }

        /// <summary>
        /// URL of the social media account.
        /// </summary>
        public string Url { get; set; }
    }
}