// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A file in a gist.
	/// </summary>
	public class GistFile
	{
		/// <summary>
		/// The file name encoded to remove characters that are invalid in URL paths.
		/// </summary>
		public string EncodedName { get; set; }

		/// <summary>
		/// The gist file encoding.
		/// </summary>
		public string Encoding { get; set; }

		/// <summary>
		/// The file extension from the file name.
		/// </summary>
		public string Extension { get; set; }

		/// <summary>
		/// Indicates if this file is an image.
		/// </summary>
		public bool IsImage { get; set; }

		/// <summary>
		/// Whether the file's contents were truncated.
		/// </summary>
		public bool IsTruncated { get; set; }

		/// <summary>
		/// The programming language this file is written in.
		/// </summary>
		public Language Language { get; set; }

		/// <summary>
		/// The gist file name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The gist file size in bytes.
		/// </summary>
		public int? Size { get; set; }

		/// <summary>
		/// UTF8 text data or null if the file is binary
		/// </summary>
		/// <param name="truncate">Optionally truncate the returned file to this length.</param>
		public string Text { get; set; }
	}
}
