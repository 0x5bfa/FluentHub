namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A user is an individual's account on GitHub that owns repositories and can make new content.
    /// </summary>
    public class UserSearch
    {
        /// <summary>
        /// Determine if this repository owner has any items that can be pinned to their profile.
        /// </summary>
        /// <param name="type">Filter to only a particular kind of pinnable item.</param>
        public bool AlreadyFollowed { get; set; }
        
        public string RealName { get; set; }
        
        public string Username { get; set; }
        
        public string Email { get; set; }
        
        public string Description { get; set; }
        
        public string Location { get; set; }
        
        public string Avatar { get; set; }
    }
}