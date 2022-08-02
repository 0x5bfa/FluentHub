namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a given language found in repositories.
    /// </summary>
    public class Language
    {
        /// <summary>
        /// The color defined for the current language.
        /// </summary>
        public string Color { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The name of the current language.
        /// </summary>
        public string Name { get; set; }
    }
}