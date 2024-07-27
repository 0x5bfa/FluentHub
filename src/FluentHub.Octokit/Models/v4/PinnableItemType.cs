// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents items that can be pinned to a profile page or dashboard.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum PinnableItemType
	{
		/// <summary>
		/// A repository.
		/// </summary>
		[EnumMember(Value = "REPOSITORY")]
		Repository,

		/// <summary>
		/// A gist.
		/// </summary>
		[EnumMember(Value = "GIST")]
		Gist,

		/// <summary>
		/// An issue.
		/// </summary>
		[EnumMember(Value = "ISSUE")]
		Issue,

		/// <summary>
		/// A project.
		/// </summary>
		[EnumMember(Value = "PROJECT")]
		Project,

		/// <summary>
		/// A pull request.
		/// </summary>
		[EnumMember(Value = "PULL_REQUEST")]
		PullRequest,

		/// <summary>
		/// A user.
		/// </summary>
		[EnumMember(Value = "USER")]
		User,

		/// <summary>
		/// An organization.
		/// </summary>
		[EnumMember(Value = "ORGANIZATION")]
		Organization,

		/// <summary>
		/// A team.
		/// </summary>
		[EnumMember(Value = "TEAM")]
		Team,
	}
}
