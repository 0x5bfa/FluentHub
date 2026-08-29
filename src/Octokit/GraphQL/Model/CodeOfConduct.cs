namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The Code of Conduct for a repository
    /// </summary>
    public class CodeOfConduct : QueryableValue<CodeOfConduct>
    {
        internal CodeOfConduct(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The body of the Code of Conduct
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The Node ID of the CodeOfConduct object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The key for the Code of Conduct
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// The formal name of the Code of Conduct
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The HTTP path for this Code of Conduct
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The HTTP URL for this Code of Conduct
        /// </summary>
        public string Url { get; }

        internal static CodeOfConduct Create(Expression expression)
        {
            return new CodeOfConduct(expression);
        }
    }
}