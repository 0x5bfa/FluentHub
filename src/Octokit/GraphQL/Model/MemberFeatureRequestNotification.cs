namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a member feature request notification
    /// </summary>
    public class MemberFeatureRequestNotification : QueryableValue<MemberFeatureRequestNotification>
    {
        internal MemberFeatureRequestNotification(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Represents member feature request body containing organization name and the number of feature requests
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The Node ID of the MemberFeatureRequestNotification object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Represents member feature request notification title
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static MemberFeatureRequestNotification Create(Expression expression)
        {
            return new MemberFeatureRequestNotification(expression);
        }
    }
}