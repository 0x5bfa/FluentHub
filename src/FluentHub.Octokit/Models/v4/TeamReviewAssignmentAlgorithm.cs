// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible team review assignment algorithms
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TeamReviewAssignmentAlgorithm
	{
		/// <summary>
		/// Alternate reviews between each team member
		/// </summary>
		[EnumMember(Value = "ROUND_ROBIN")]
		RoundRobin,

		/// <summary>
		/// Balance review load across the entire team
		/// </summary>
		[EnumMember(Value = "LOAD_BALANCE")]
		LoadBalance,
	}
}
