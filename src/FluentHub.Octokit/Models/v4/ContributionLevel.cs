// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Varying levels of contributions from none to many.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ContributionLevel
	{
		/// <summary>
		/// No contributions occurred.
		/// </summary>
		[EnumMember(Value = "NONE")]
		None,

		/// <summary>
		/// Lowest 25% of days of contributions.
		/// </summary>
		[EnumMember(Value = "FIRST_QUARTILE")]
		FirstQuartile,

		/// <summary>
		/// Second lowest 25% of days of contributions. More contributions than the first quartile.
		/// </summary>
		[EnumMember(Value = "SECOND_QUARTILE")]
		SecondQuartile,

		/// <summary>
		/// Second highest 25% of days of contributions. More contributions than second quartile, less than the fourth quartile.
		/// </summary>
		[EnumMember(Value = "THIRD_QUARTILE")]
		ThirdQuartile,

		/// <summary>
		/// Highest 25% of days of contributions. More contributions than the third quartile.
		/// </summary>
		[EnumMember(Value = "FOURTH_QUARTILE")]
		FourthQuartile,
	}
}
