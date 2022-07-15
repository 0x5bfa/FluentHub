namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// The value of a repository field in a Project item.
    /// </summary>
    public class ProjectV2ItemFieldRepositoryValue
    {
        /// <summary>
        /// The field that contains this value.
        /// </summary>
        public ProjectV2FieldConfiguration Field { get; set; }

        /// <summary>
        /// The repository for this field.
        /// </summary>
        public Repository Repository { get; set; }
    }
}