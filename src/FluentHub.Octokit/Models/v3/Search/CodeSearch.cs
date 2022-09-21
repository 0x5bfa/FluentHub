namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A summary of a repository.
    /// </summary>
    public class CodeSearch
    {
        /// <summary>
        /// The description of the repository.
        /// </summary>
        public OctokitV3.Repository Repo { get; set; }
        
        public string Language { get; set; }
        
        public string MatchesShowing { get; set; }
        
        public string LastIndexed { get; set; }
        
        public string Code { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
