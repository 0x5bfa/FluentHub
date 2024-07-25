// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The display color of a single-select field option.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ProjectV2SingleSelectFieldOptionColor
	{
		/// <summary>
		/// GRAY
		/// </summary>
		[EnumMember(Value = "GRAY")]
		Gray,

		/// <summary>
		/// BLUE
		/// </summary>
		[EnumMember(Value = "BLUE")]
		Blue,

		/// <summary>
		/// GREEN
		/// </summary>
		[EnumMember(Value = "GREEN")]
		Green,

		/// <summary>
		/// YELLOW
		/// </summary>
		[EnumMember(Value = "YELLOW")]
		Yellow,

		/// <summary>
		/// ORANGE
		/// </summary>
		[EnumMember(Value = "ORANGE")]
		Orange,

		/// <summary>
		/// RED
		/// </summary>
		[EnumMember(Value = "RED")]
		Red,

		/// <summary>
		/// PINK
		/// </summary>
		[EnumMember(Value = "PINK")]
		Pink,

		/// <summary>
		/// PURPLE
		/// </summary>
		[EnumMember(Value = "PURPLE")]
		Purple,
	}
}
