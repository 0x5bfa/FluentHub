namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Information extracted from a repository's `CODEOWNERS` file.
    /// </summary>
    public class RepositoryCodeowners
    {
        /// <summary>
        /// Any problems that were encountered while parsing the `CODEOWNERS` file.
        /// </summary>
        public List<RepositoryCodeownersError> Errors { get; set; }
    }
}