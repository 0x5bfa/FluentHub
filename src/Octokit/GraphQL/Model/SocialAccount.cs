namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Social media profile associated with a user.
    /// </summary>
    public class SocialAccount : QueryableValue<SocialAccount>
    {
        internal SocialAccount(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Name of the social media account as it appears on the profile.
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Software or company that hosts the social media account.
        /// </summary>
        public SocialAccountProvider Provider { get; }

        /// <summary>
        /// URL of the social media account.
        /// </summary>
        public string Url { get; }

        internal static SocialAccount Create(Expression expression)
        {
            return new SocialAccount(expression);
        }
    }
}