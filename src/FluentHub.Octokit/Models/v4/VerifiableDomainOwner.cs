namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Types that can own a verifiable domain.
    /// </summary>
    public class VerifiableDomainOwner
    {
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