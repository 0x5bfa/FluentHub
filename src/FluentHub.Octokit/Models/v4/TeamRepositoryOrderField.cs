// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which team repository connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TeamRepositoryOrderField
	{
		/// <summary>
		/// Order repositories by creation time
		/// </summary>
		[EnumMember(Value = "CREATED_AT")]
		CreatedAt,

		/// <summary>
		/// Order repositories by update time
		/// </summary>
		[EnumMember(Value = "UPDATED_AT")]
		UpdatedAt,

		/// <summary>
		/// Order repositories by push time
		/// </summary>
		[EnumMember(Value = "PUSHED_AT")]
		PushedAt,

		/// <summary>
		/// Order repositories by name
		/// </summary>
		[EnumMember(Value = "NAME")]
		Name,

		/// <summary>
		/// Order repositories by permission
		/// </summary>
		[EnumMember(Value = "PERMISSION")]
		Permission,

		/// <summary>
		/// Order repositories by number of stargazers
		/// </summary>
		[EnumMember(Value = "STARGAZERS")]
		Stargazers,
	}
}
