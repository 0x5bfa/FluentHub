namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents the language of a repository.
    /// </summary>
    public class LanguageEdge
    {
        public string Cursor { get; set; }

        public Language Node { get; set; }

        /// <summary>
        /// The number of bytes of code written in the language.
        /// </summary>
        public int Size { get; set; }
    }
}