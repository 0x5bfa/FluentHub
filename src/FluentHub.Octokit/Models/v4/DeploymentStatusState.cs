// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible states for a deployment status.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DeploymentStatusState
	{
		/// <summary>
		/// The deployment is pending.
		/// </summary>
		[EnumMember(Value = "PENDING")]
		Pending,

		/// <summary>
		/// The deployment was successful.
		/// </summary>
		[EnumMember(Value = "SUCCESS")]
		Success,

		/// <summary>
		/// The deployment has failed.
		/// </summary>
		[EnumMember(Value = "FAILURE")]
		Failure,

		/// <summary>
		/// The deployment is inactive.
		/// </summary>
		[EnumMember(Value = "INACTIVE")]
		Inactive,

		/// <summary>
		/// The deployment experienced an error.
		/// </summary>
		[EnumMember(Value = "ERROR")]
		Error,

		/// <summary>
		/// The deployment is queued
		/// </summary>
		[EnumMember(Value = "QUEUED")]
		Queued,

		/// <summary>
		/// The deployment is in progress.
		/// </summary>
		[EnumMember(Value = "IN_PROGRESS")]
		InProgress,

		/// <summary>
		/// The deployment is waiting.
		/// </summary>
		[EnumMember(Value = "WAITING")]
		Waiting,
	}
}
