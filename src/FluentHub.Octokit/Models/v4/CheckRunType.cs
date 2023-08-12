// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible types of check runs.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum CheckRunType
	{
		/// <summary>
		/// Every check run available.
		/// </summary>
		[EnumMember(Value = "ALL")]
		All,

		/// <summary>
		/// The latest check run.
		/// </summary>
		[EnumMember(Value = "LATEST")]
		Latest,
	}
}
