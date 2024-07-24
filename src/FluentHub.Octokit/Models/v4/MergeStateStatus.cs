// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Detailed status information about a pull request merge.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MergeStateStatus
	{
		/// <summary>
		/// The merge commit cannot be cleanly created.
		/// </summary>
		[EnumMember(Value = "DIRTY")]
		Dirty,

		/// <summary>
		/// The state cannot currently be determined.
		/// </summary>
		[EnumMember(Value = "UNKNOWN")]
		Unknown,

		/// <summary>
		/// The merge is blocked.
		/// </summary>
		[EnumMember(Value = "BLOCKED")]
		Blocked,

		/// <summary>
		/// The head ref is out of date.
		/// </summary>
		[EnumMember(Value = "BEHIND")]
		Behind,

		/// <summary>
		/// The merge is blocked due to the pull request being a draft.
		/// </summary>
		[Obsolete(@"DRAFT state will be removed from this enum and `isDraft` should be used instead Use PullRequest.isDraft instead. Removal on 2021-01-01 UTC.")]
		[EnumMember(Value = "DRAFT")]
		Draft,

		/// <summary>
		/// Mergeable with non-passing commit status.
		/// </summary>
		[EnumMember(Value = "UNSTABLE")]
		Unstable,

		/// <summary>
		/// Mergeable with passing commit status and pre-receive hooks.
		/// </summary>
		[EnumMember(Value = "HAS_HOOKS")]
		HasHooks,

		/// <summary>
		/// Mergeable and passing commit status.
		/// </summary>
		[EnumMember(Value = "CLEAN")]
		Clean,
	}
}
