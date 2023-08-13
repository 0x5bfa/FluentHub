// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The type of a project item.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ProjectV2ItemType
	{
		/// <summary>
		/// Issue
		/// </summary>
		[EnumMember(Value = "ISSUE")]
		Issue,

		/// <summary>
		/// Pull Request
		/// </summary>
		[EnumMember(Value = "PULL_REQUEST")]
		PullRequest,

		/// <summary>
		/// Draft Issue
		/// </summary>
		[EnumMember(Value = "DRAFT_ISSUE")]
		DraftIssue,

		/// <summary>
		/// Redacted Item
		/// </summary>
		[EnumMember(Value = "REDACTED")]
		Redacted,
	}
}
