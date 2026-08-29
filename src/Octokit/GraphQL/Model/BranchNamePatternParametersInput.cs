namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Parameters to be used for the branch_name_pattern rule
    /// </summary>
    public class BranchNamePatternParametersInput
    {
        /// <summary>
        /// How this rule will appear to users.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// If true, the rule will fail if the pattern matches.
        /// </summary>
        public bool? Negate { get; set; }

        /// <summary>
        /// The operator to use for matching.
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// The pattern to match with.
        /// </summary>
        public string Pattern { get; set; }
    }
}