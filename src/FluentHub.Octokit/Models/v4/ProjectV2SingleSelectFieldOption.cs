namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Single select field option for a configuration for a project.
    /// </summary>
    public class ProjectV2SingleSelectFieldOption
    {
        /// <summary>
        /// The option's ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The option's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The option's html name.
        /// </summary>
        public string NameHTML { get; set; }
    }
}