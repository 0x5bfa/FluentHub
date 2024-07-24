// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which language connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum LanguageOrderField
	{
		/// <summary>
		/// Order languages by the size of all files containing the language
		/// </summary>
		[EnumMember(Value = "SIZE")]
		Size,
	}
}
