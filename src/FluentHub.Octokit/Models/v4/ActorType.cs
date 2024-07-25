// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The actor's type.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ActorType
	{
		/// <summary>
		/// Indicates a user actor.
		/// </summary>
		[EnumMember(Value = "USER")]
		User,

		/// <summary>
		/// Indicates a team actor.
		/// </summary>
		[EnumMember(Value = "TEAM")]
		Team,
	}
}
