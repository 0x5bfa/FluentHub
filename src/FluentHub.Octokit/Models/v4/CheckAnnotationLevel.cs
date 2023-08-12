// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents an annotation's information level.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum CheckAnnotationLevel
	{
		/// <summary>
		/// An annotation indicating an inescapable error.
		/// </summary>
		[EnumMember(Value = "FAILURE")]
		Failure,

		/// <summary>
		/// An annotation indicating some information.
		/// </summary>
		[EnumMember(Value = "NOTICE")]
		Notice,

		/// <summary>
		/// An annotation indicating an ignorable error.
		/// </summary>
		[EnumMember(Value = "WARNING")]
		Warning,
	}
}
