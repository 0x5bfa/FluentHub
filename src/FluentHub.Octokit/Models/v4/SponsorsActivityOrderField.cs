// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which GitHub Sponsors activity connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SponsorsActivityOrderField
	{
		/// <summary>
		/// Order activities by when they happened.
		/// </summary>
		[EnumMember(Value = "TIMESTAMP")]
		Timestamp,
	}
}
