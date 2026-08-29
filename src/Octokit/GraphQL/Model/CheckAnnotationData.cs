namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information from a check run analysis to specific lines of code.
    /// </summary>
    public class CheckAnnotationData
    {
        /// <summary>
        /// The path of the file to add an annotation to.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The location of the annotation
        /// </summary>
        public CheckAnnotationRange Location { get; set; }

        /// <summary>
        /// Represents an annotation's information level
        /// </summary>
        public CheckAnnotationLevel AnnotationLevel { get; set; }

        /// <summary>
        /// A short description of the feedback for these lines of code.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The title that represents the annotation.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Details about this annotation.
        /// </summary>
        public string RawDetails { get; set; }
    }
}