namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an SSH signature on a Commit or Tag.
    /// </summary>
    public class SshSignature : QueryableValue<SshSignature>
    {
        internal SshSignature(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Email used to sign this object.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// True if the signature is valid and verified by GitHub.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Hex-encoded fingerprint of the key that signed this object.
        /// </summary>
        public string KeyFingerprint { get; }

        /// <summary>
        /// Payload for GPG signing object. Raw ODB object without the signature header.
        /// </summary>
        public string Payload { get; }

        /// <summary>
        /// ASCII-armored signature header from object.
        /// </summary>
        public string Signature { get; }

        /// <summary>
        /// GitHub user corresponding to the email signing this commit.
        /// </summary>
        public User Signer => this.CreateProperty(x => x.Signer, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The state of this signature. `VALID` if signature is valid and verified by GitHub, otherwise represents reason why signature is considered invalid.
        /// </summary>
        public GitSignatureState State { get; }

        /// <summary>
        /// True if the signature was made with GitHub's signing key.
        /// </summary>
        public bool WasSignedByGitHub { get; }

        internal static SshSignature Create(Expression expression)
        {
            return new SshSignature(expression);
        }
    }
}