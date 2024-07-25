// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which sponsorable connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SponsorableOrderField
	{
		/// <summary>
		/// Order sponsorable entities by login (username).
		/// </summary>
		[EnumMember(Value = "LOGIN")]
		Login,
	}
}
