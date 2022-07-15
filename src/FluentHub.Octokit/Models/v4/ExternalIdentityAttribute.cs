namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An attribute for the External Identity attributes collection
    /// </summary>
    public class ExternalIdentityAttribute
    {
        /// <summary>
        /// The attribute metadata as JSON
        /// </summary>
        public string Metadata { get; set; }

        /// <summary>
        /// The attribute name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The attribute value
        /// </summary>
        public string Value { get; set; }
    }
}