namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A User who is an administrator of an enterprise.
    /// </summary>
    public class EnterpriseAdministratorEdge
    {
        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; set; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public User Node { get; set; }

        /// <summary>
        /// The role of the administrator.
        /// </summary>
        public EnterpriseAdministratorRole Role { get; set; }
    }
}