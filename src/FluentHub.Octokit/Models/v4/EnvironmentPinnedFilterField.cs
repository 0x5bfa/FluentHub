// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which environments connections can be ordered
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum EnvironmentPinnedFilterField
	{
		/// <summary>
		/// All environments will be returned.
		/// </summary>
		[EnumMember(Value = "ALL")]
		All,

		/// <summary>
		/// Only pinned environment will be returned
		/// </summary>
		[EnumMember(Value = "ONLY")]
		Only,

		/// <summary>
		/// Environments exclude pinned will be returned
		/// </summary>
		[EnumMember(Value = "NONE")]
		None,
	}
}
