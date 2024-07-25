// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible states for a deployment review.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DeploymentReviewState
	{
		/// <summary>
		/// The deployment was approved.
		/// </summary>
		[EnumMember(Value = "APPROVED")]
		Approved,

		/// <summary>
		/// The deployment was rejected.
		/// </summary>
		[EnumMember(Value = "REJECTED")]
		Rejected,
	}
}
