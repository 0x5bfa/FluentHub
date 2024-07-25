// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which commit contribution connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum CommitContributionOrderField
	{
		/// <summary>
		/// Order commit contributions by when they were made.
		/// </summary>
		[EnumMember(Value = "OCCURRED_AT")]
		OccurredAt,

		/// <summary>
		/// Order commit contributions by how many commits they represent.
		/// </summary>
		[EnumMember(Value = "COMMIT_COUNT")]
		CommitCount,
	}
}
