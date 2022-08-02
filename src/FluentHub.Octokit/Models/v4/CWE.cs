namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A common weakness enumeration
    /// </summary>
    public class CWE
    {
        /// <summary>
        /// The id of the CWE
        /// </summary>
        public string CweId { get; set; }

        /// <summary>
        /// A detailed description of this CWE
        /// </summary>
        public string Description { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The name of this CWE
        /// </summary>
        public string Name { get; set; }
    }
}