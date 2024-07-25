// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// Information about a signature (GPG or S/MIME) on a Commit or Tag.
	/// </summary>
	public interface IGitSignature
	{
		/// <summary>
		/// Email used to sign this object.
		/// </summary>
		string Email { get; set; }

		/// <summary>
		/// True if the signature is valid and verified by GitHub.
		/// </summary>
		bool IsValid { get; set; }

		/// <summary>
		/// Payload for GPG signing object. Raw ODB object without the signature header.
		/// </summary>
		string Payload { get; set; }

		/// <summary>
		/// ASCII-armored signature header from object.
		/// </summary>
		string Signature { get; set; }

		/// <summary>
		/// GitHub user corresponding to the email signing this commit.
		/// </summary>
		User Signer { get; set; }

		/// <summary>
		/// The state of this signature. `VALID` if signature is valid and verified by GitHub, otherwise represents reason why signature is considered invalid.
		/// </summary>
		GitSignatureState State { get; set; }

		/// <summary>
		/// True if the signature was made with GitHub's signing key.
		/// </summary>
		bool WasSignedByGitHub { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class GitSignature : IGitSignature
	{
		public string Email { get; set; }

		public bool IsValid { get; set; }

		public string Payload { get; set; }

		public string Signature { get; set; }

		public User Signer { get; set; }

		public GitSignatureState State { get; set; }

		public bool WasSignedByGitHub { get; set; }
	}
}

