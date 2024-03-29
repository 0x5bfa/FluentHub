// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Reason that the suggested topic is declined.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TopicSuggestionDeclineReason
	{
		/// <summary>
		/// The suggested topic is not relevant to the repository.
		/// </summary>
		[EnumMember(Value = "NOT_RELEVANT")]
		NotRelevant,

		/// <summary>
		/// The suggested topic is too specific for the repository (e.g. #ruby-on-rails-version-4-2-1).
		/// </summary>
		[EnumMember(Value = "TOO_SPECIFIC")]
		TooSpecific,

		/// <summary>
		/// The viewer does not like the suggested topic.
		/// </summary>
		[EnumMember(Value = "PERSONAL_PREFERENCE")]
		PersonalPreference,

		/// <summary>
		/// The suggested topic is too general for the repository.
		/// </summary>
		[EnumMember(Value = "TOO_GENERAL")]
		TooGeneral,
	}
}
