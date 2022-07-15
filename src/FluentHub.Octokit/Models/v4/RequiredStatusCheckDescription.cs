namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a required status check for a protected branch, but not any specific run of that check.
    /// </summary>
    public class RequiredStatusCheckDescription
    {
        /// <summary>
        /// The App that must provide this status in order for it to be accepted.
        /// </summary>
        public App App { get; set; }

        /// <summary>
        /// The name of this status.
        /// </summary>
        public string Context { get; set; }
    }
}