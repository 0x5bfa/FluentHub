// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible commit status states.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum StatusState
	{
		/// <summary>
		/// Status is expected.
		/// </summary>
		[EnumMember(Value = "EXPECTED")]
		Expected,

		/// <summary>
		/// Status is errored.
		/// </summary>
		[EnumMember(Value = "ERROR")]
		Error,

		/// <summary>
		/// Status is failing.
		/// </summary>
		[EnumMember(Value = "FAILURE")]
		Failure,

		/// <summary>
		/// Status is pending.
		/// </summary>
		[EnumMember(Value = "PENDING")]
		Pending,

		/// <summary>
		/// Status is successful.
		/// </summary>
		[EnumMember(Value = "SUCCESS")]
		Success,
	}
}
