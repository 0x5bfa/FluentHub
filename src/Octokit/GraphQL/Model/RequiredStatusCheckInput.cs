namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Specifies the attributes for a new or updated required status check.
    /// </summary>
    public class RequiredStatusCheckInput
    {
        /// <summary>
        /// Status check context that must pass for commits to be accepted to the matching branch.
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// The ID of the App that must set the status in order for it to be accepted. Omit this value to use whichever app has recently been setting this status, or use "any" to allow any app to set the status.
        /// </summary>
        public ID? AppId { get; set; }
    }
}