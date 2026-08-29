namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An error in a `CODEOWNERS` file.
    /// </summary>
    public class RepositoryCodeownersError : QueryableValue<RepositoryCodeownersError>
    {
        internal RepositoryCodeownersError(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The column number where the error occurs.
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// A short string describing the type of error.
        /// </summary>
        public string Kind { get; }

        /// <summary>
        /// The line number where the error occurs.
        /// </summary>
        public int Line { get; }

        /// <summary>
        /// A complete description of the error, combining information from other fields.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// The path to the file when the error occurs.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The content of the line where the error occurs.
        /// </summary>
        public string Source { get; }

        /// <summary>
        /// A suggestion of how to fix the error.
        /// </summary>
        public string Suggestion { get; }

        internal static RepositoryCodeownersError Create(Expression expression)
        {
            return new RepositoryCodeownersError(expression);
        }
    }
}