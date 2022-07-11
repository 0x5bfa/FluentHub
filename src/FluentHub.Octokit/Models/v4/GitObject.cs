namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a Git object.
    /// </summary>
    public interface IGitObject
    {
        /// <summary>
        /// An abbreviated version of the Git object ID
        /// </summary>
        string AbbreviatedOid { get; set; }

        /// <summary>
        /// The HTTP path for this Git object
        /// </summary>
        string CommitResourcePath { get; set; }

        /// <summary>
        /// The HTTP URL for this Git object
        /// </summary>
        string CommitUrl { get; set; }

        ID Id { get; set; }

        /// <summary>
        /// The Git object ID
        /// </summary>
        string Oid { get; set; }

        /// <summary>
        /// The Repository the Git object belongs to
        /// </summary>
        Repository Repository { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubIGitObject
    {
        

        public string AbbreviatedOid { get; set; }

        public string CommitResourcePath { get; set; }

        public string CommitUrl { get; set; }

        public ID Id { get; set; }

        public string Oid { get; set; }

        public Repository Repository { get; set; }
    }
}