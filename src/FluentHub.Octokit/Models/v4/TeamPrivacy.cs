// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible team privacy values.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TeamPrivacy
	{
		/// <summary>
		/// A secret team can only be seen by its members.
		/// </summary>
		[EnumMember(Value = "SECRET")]
		Secret,

		/// <summary>
		/// A visible team can be seen and @mentioned by every member of the organization.
		/// </summary>
		[EnumMember(Value = "VISIBLE")]
		Visible,
	}
}
