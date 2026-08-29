namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for Audit Log connections.
    /// </summary>
    public class AuditLogOrder
    {
        /// <summary>
        /// The field to order Audit Logs by.
        /// </summary>
        public AuditLogOrderField? Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection? Direction { get; set; }
    }
}