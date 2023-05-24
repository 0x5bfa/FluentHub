namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Parameters to be used for the update rule
    /// </summary>
    public class UpdateParameters
    {
        /// <summary>
        /// Branch can pull changes from its upstream repository
        /// </summary>
        public bool UpdateAllowsFetchAndMerge { get; set; }
    }
}