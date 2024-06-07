// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a Git blob.
	/// </summary>
	public class Blob
	{
		/// <summary>
		/// An abbreviated version of the Git object ID
		/// </summary>
		public string AbbreviatedOid { get; set; }

		/// <summary>
		/// Byte size of Blob object
		/// </summary>
		public int ByteSize { get; set; }

		/// <summary>
		/// The HTTP path for this Git object
		/// </summary>
		public string CommitResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for this Git object
		/// </summary>
		public string CommitUrl { get; set; }

		/// <summary>
		/// The Node ID of the Blob object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Indicates whether the Blob is binary or text. Returns null if unable to determine the encoding.
		/// </summary>
		public bool? IsBinary { get; set; }

		/// <summary>
		/// Indicates whether the contents is truncated
		/// </summary>
		public bool IsTruncated { get; set; }

		/// <summary>
		/// The Git object ID
		/// </summary>
		public string Oid { get; set; }

		/// <summary>
		/// The Repository the Git object belongs to
		/// </summary>
		public Repository Repository { get; set; }

		/// <summary>
		/// UTF8 text data or null if the Blob is binary
		/// </summary>
		public string Text { get; set; }
	}
}
