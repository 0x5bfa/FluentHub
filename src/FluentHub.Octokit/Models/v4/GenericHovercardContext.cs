namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A generic hovercard context with a message and icon
    /// </summary>
    public class GenericHovercardContext
    {
        /// <summary>
        /// A string describing this context
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// An octicon to accompany this context
        /// </summary>
        public string Octicon { get; set; }
    }
}