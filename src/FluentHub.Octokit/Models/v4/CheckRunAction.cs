namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Possible further actions the integrator can perform.
    /// </summary>
    public class CheckRunAction
    {
        /// <summary>
        /// The text to be displayed on a button in the web UI.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// A short explanation of what this action would do.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A reference for the action on the integrator's system. 
        /// </summary>
        public string Identifier { get; set; }
    }
}