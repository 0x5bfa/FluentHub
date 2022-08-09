namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Types that can own an IP allow list.
    /// </summary>
    public class IpAllowListOwner
    {
        /// <summary>
        /// A GitHub App.
        /// </summary>
        public App App { get; set; }

        /// <summary>
        /// An account to manage multiple organizations with consolidated policy and billing.
        /// </summary>
        public Enterprise Enterprise { get; set; }

        /// <summary>
        /// An account on GitHub, with one or more owners, that has repositories, members and teams.
        /// </summary>
        public Organization Organization { get; set; }
    }
}