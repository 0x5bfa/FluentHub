namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents an unknown signature on a Commit or Tag.
    /// </summary>
    public class UnknownSignature
    {
        /// <summary>
        /// Email used to sign this object.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// True if the signature is valid and verified by GitHub.
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Payload for GPG signing object. Raw ODB object without the signature header.
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// ASCII-armored signature header from object.
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// GitHub user corresponding to the email signing this commit.
        /// </summary>
        public User Signer { get; set; }

        /// <summary>
        /// The state of this signature. `VALID` if signature is valid and verified by GitHub, otherwise represents reason why signature is considered invalid.
        /// </summary>
        public GitSignatureState State { get; set; }

        /// <summary>
        /// True if the signature was made with GitHub's signing key.
        /// </summary>
        public bool WasSignedByGitHub { get; set; }
    }
}