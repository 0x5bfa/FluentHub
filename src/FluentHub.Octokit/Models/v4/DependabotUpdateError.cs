namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An error produced from a Dependabot Update
    /// </summary>
    public class DependabotUpdateError
    {
        /// <summary>
        /// The body of the error
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The error code
        /// </summary>
        public string ErrorType { get; set; }

        /// <summary>
        /// The title of the error
        /// </summary>
        public string Title { get; set; }
    }
}