namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// The Code of Conduct for a repository
    /// </summary>
    public class CodeOfConduct
    {
        /// <summary>
        /// The body of the Code of Conduct
        /// </summary>
        public string Body { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The key for the Code of Conduct
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The formal name of the Code of Conduct
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The HTTP path for this Code of Conduct
        /// </summary>
        public string ResourcePath { get; set; }

        /// <summary>
        /// The HTTP URL for this Code of Conduct
        /// </summary>
        public string Url { get; set; }
    }
}