namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A single check annotation.
    /// </summary>
    public class CheckAnnotation : QueryableValue<CheckAnnotation>
    {
        internal CheckAnnotation(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The annotation's severity level.
        /// </summary>
        public CheckAnnotationLevel? AnnotationLevel { get; }

        /// <summary>
        /// The path to the file that this annotation was made on.
        /// </summary>
        public string BlobUrl { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The position of this annotation.
        /// </summary>
        public CheckAnnotationSpan Location => this.CreateProperty(x => x.Location, Octokit.GraphQL.Model.CheckAnnotationSpan.Create);

        /// <summary>
        /// The annotation's message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// The path that this annotation was made on.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Additional information about the annotation.
        /// </summary>
        public string RawDetails { get; }

        /// <summary>
        /// The annotation's title
        /// </summary>
        public string Title { get; }

        internal static CheckAnnotation Create(Expression expression)
        {
            return new CheckAnnotation(expression);
        }
    }
}