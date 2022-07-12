namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Describes a License's conditions, permissions, and limitations
    /// </summary>
    public class LicenseRule
    {
        /// <summary>
        /// A description of the rule
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The machine-readable rule key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The human-readable rule label
        /// </summary>
        public string Label { get; set; }
    }
}