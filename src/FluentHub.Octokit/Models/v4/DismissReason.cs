// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible reasons that a Dependabot alert was dismissed.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DismissReason
	{
		/// <summary>
		/// A fix has already been started
		/// </summary>
		[EnumMember(Value = "FIX_STARTED")]
		FixStarted,

		/// <summary>
		/// No bandwidth to fix this
		/// </summary>
		[EnumMember(Value = "NO_BANDWIDTH")]
		NoBandwidth,

		/// <summary>
		/// Risk is tolerable to this project
		/// </summary>
		[EnumMember(Value = "TOLERABLE_RISK")]
		TolerableRisk,

		/// <summary>
		/// This alert is inaccurate or incorrect
		/// </summary>
		[EnumMember(Value = "INACCURATE")]
		Inaccurate,

		/// <summary>
		/// Vulnerable code is not actually used
		/// </summary>
		[EnumMember(Value = "NOT_USED")]
		NotUsed,
	}
}
