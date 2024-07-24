// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a Git blame.
	/// </summary>
	public class Blame
	{
		/// <summary>
		/// The list of ranges from a Git blame.
		/// </summary>
		public List<BlameRange> Ranges { get; set; }
	}
}
