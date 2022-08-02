namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Email attributes from External Identity
    /// </summary>
    public class UserEmailMetadata
    {
        /// <summary>
        /// Boolean to identify primary emails
        /// </summary>
        public bool? Primary { get; set; }

        /// <summary>
        /// Type of email
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Email id
        /// </summary>
        public string Value { get; set; }
    }
}