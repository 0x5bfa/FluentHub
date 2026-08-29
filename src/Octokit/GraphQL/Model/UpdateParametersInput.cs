namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Only allow users with bypass permission to update matching refs.
    /// </summary>
    public class UpdateParametersInput
    {
        /// <summary>
        /// Branch can pull changes from its upstream repository
        /// </summary>
        public bool UpdateAllowsFetchAndMerge { get; set; }
    }
}