namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A command to add a file at the given path with the given contents as part of a commit.  Any existing file at that that path will be replaced.
    /// </summary>
    public class FileAddition
    {
        /// <summary>
        /// The path in the repository where the file will be located
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The base64 encoded contents of the file
        /// </summary>
        public string Contents { get; set; }
    }
}