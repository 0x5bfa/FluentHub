// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a range of information from a Git blame.
	/// </summary>
	public class BlameRange
	{
		/// <summary>
		/// Identifies the recency of the change, from 1 (new) to 10 (old). This is calculated as a 2-quantile and determines the length of distance between the median age of all the changes in the file and the recency of the current range's change.
		/// </summary>
		public int Age { get; set; }

		/// <summary>
		/// Identifies the line author
		/// </summary>
		public Commit Commit { get; set; }

		/// <summary>
		/// The ending line for the range
		/// </summary>
		public int EndingLine { get; set; }

		/// <summary>
		/// The starting line for the range
		/// </summary>
		public int StartingLine { get; set; }
	}
}
