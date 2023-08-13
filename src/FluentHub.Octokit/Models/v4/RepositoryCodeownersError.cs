// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An error in a `CODEOWNERS` file.
	/// </summary>
	public class RepositoryCodeownersError
	{
		/// <summary>
		/// The column number where the error occurs.
		/// </summary>
		public int Column { get; set; }

		/// <summary>
		/// A short string describing the type of error.
		/// </summary>
		public string Kind { get; set; }

		/// <summary>
		/// The line number where the error occurs.
		/// </summary>
		public int Line { get; set; }

		/// <summary>
		/// A complete description of the error, combining information from other fields.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// The path to the file when the error occurs.
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// The content of the line where the error occurs.
		/// </summary>
		public string Source { get; set; }

		/// <summary>
		/// A suggestion of how to fix the error.
		/// </summary>
		public string Suggestion { get; set; }
	}
}
