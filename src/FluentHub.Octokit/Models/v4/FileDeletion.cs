namespace FluentHub.Octokit.v4.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A command to delete the file at the given path as part of a commit.
    /// </summary>
    public class FileDeletion
    {
        /// <summary>
        /// The path to delete
        /// </summary>
        public string Path { get; set; }
    }
}