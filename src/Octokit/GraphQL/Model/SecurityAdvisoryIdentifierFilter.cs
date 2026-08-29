namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An advisory identifier to filter results on.
    /// </summary>
    public class SecurityAdvisoryIdentifierFilter
    {
        /// <summary>
        /// The identifier type.
        /// </summary>
        public SecurityAdvisoryIdentifierType Type { get; set; }

        /// <summary>
        /// The identifier string. Supports exact or partial matching.
        /// </summary>
        public string Value { get; set; }
    }
}