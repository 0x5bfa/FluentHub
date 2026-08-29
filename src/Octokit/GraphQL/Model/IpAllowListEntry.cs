namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An IP address or range of addresses that is allowed to access an owner's resources.
    /// </summary>
    public class IpAllowListEntry : QueryableValue<IpAllowListEntry>
    {
        internal IpAllowListEntry(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A single IP address or range of IP addresses in CIDR notation.
        /// </summary>
        public string AllowListValue { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The Node ID of the IpAllowListEntry object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Whether the entry is currently active.
        /// </summary>
        public bool IsActive { get; }

        /// <summary>
        /// The name of the IP allow list entry.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The owner of the IP allow list entry.
        /// </summary>
        public IpAllowListOwner Owner => this.CreateProperty(x => x.Owner, Octokit.GraphQL.Model.IpAllowListOwner.Create);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static IpAllowListEntry Create(Expression expression)
        {
            return new IpAllowListEntry(expression);
        }
    }
}