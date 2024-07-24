// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible states for a check suite or run status.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum CheckStatusState
	{
		/// <summary>
		/// The check suite or run has been requested.
		/// </summary>
		[EnumMember(Value = "REQUESTED")]
		Requested,

		/// <summary>
		/// The check suite or run has been queued.
		/// </summary>
		[EnumMember(Value = "QUEUED")]
		Queued,

		/// <summary>
		/// The check suite or run is in progress.
		/// </summary>
		[EnumMember(Value = "IN_PROGRESS")]
		InProgress,

		/// <summary>
		/// The check suite or run has been completed.
		/// </summary>
		[EnumMember(Value = "COMPLETED")]
		Completed,

		/// <summary>
		/// The check suite or run is in waiting state.
		/// </summary>
		[EnumMember(Value = "WAITING")]
		Waiting,

		/// <summary>
		/// The check suite or run is in pending state.
		/// </summary>
		[EnumMember(Value = "PENDING")]
		Pending,
	}
}
