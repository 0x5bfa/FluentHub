// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which repository migrations can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RepositoryMigrationOrderField
	{
		/// <summary>
		/// Order mannequins why when they were created.
		/// </summary>
		[EnumMember(Value = "CREATED_AT")]
		CreatedAt,
	}
}
