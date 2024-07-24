// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible merging strategies for a merge queue.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MergeQueueMergingStrategy
	{
		/// <summary>
		/// Entries only allowed to merge if they are passing.
		/// </summary>
		[EnumMember(Value = "ALLGREEN")]
		Allgreen,

		/// <summary>
		/// Failing Entires are allowed to merge if they are with a passing entry.
		/// </summary>
		[EnumMember(Value = "HEADGREEN")]
		Headgreen,
	}
}
