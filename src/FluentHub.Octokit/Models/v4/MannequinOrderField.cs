// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which mannequins can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MannequinOrderField
	{
		/// <summary>
		/// Order mannequins alphabetically by their source login.
		/// </summary>
		[EnumMember(Value = "LOGIN")]
		Login,

		/// <summary>
		/// Order mannequins why when they were created.
		/// </summary>
		[EnumMember(Value = "CREATED_AT")]
		CreatedAt,
	}
}
