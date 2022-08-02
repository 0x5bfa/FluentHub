namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of CreateCommitOnBranch
    /// </summary>
    public class CreateCommitOnBranchInput
    {        /// <summary>
        /// The Ref to be updated.  Must be a branch.
        /// </summary>
        public CommittableBranch Branch { get; set; }

        /// <summary>
        /// A description of changes to files in this commit.
        /// </summary>
        public FileChanges FileChanges { get; set; }

        /// <summary>
        /// The commit message the be included with the commit.
        /// </summary>
        public CommitMessage Message { get; set; }

        /// <summary>
        /// The git commit oid expected at the head of the branch prior to the commit
        /// </summary>
        public string ExpectedHeadOid { get; set; }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }
    }
}