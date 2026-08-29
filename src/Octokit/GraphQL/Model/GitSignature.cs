namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Information about a signature (GPG or S/MIME) on a Commit or Tag.
    /// </summary>
    [GraphQLIdentifier("GitSignature")]
    public interface IGitSignature : IQueryableValue<IGitSignature>, IQueryableInterface
    {
        /// <summary>
        /// Email used to sign this object.
        /// </summary>
        string Email { get; }

        /// <summary>
        /// True if the signature is valid and verified by GitHub.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Payload for GPG signing object. Raw ODB object without the signature header.
        /// </summary>
        string Payload { get; }

        /// <summary>
        /// ASCII-armored signature header from object.
        /// </summary>
        string Signature { get; }

        /// <summary>
        /// GitHub user corresponding to the email signing this commit.
        /// </summary>
        User Signer { get; }

        /// <summary>
        /// The state of this signature. `VALID` if signature is valid and verified by GitHub, otherwise represents reason why signature is considered invalid.
        /// </summary>
        GitSignatureState State { get; }

        /// <summary>
        /// True if the signature was made with GitHub's signing key.
        /// </summary>
        bool WasSignedByGitHub { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("GitSignature")]
    internal class StubIGitSignature : QueryableValue<StubIGitSignature>, IGitSignature
    {
        internal StubIGitSignature(Expression expression) : base(expression)
        {
        }

        public string Email { get; }

        public bool IsValid { get; }

        public string Payload { get; }

        public string Signature { get; }

        public User Signer => this.CreateProperty(x => x.Signer, Octokit.GraphQL.Model.User.Create);

        public GitSignatureState State { get; }

        public bool WasSignedByGitHub { get; }

        internal static StubIGitSignature Create(Expression expression)
        {
            return new StubIGitSignature(expression);
        }
    }
}