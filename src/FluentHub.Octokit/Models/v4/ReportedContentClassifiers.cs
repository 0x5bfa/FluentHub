// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The reasons a piece of content can be reported or minimized.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ReportedContentClassifiers
	{
		/// <summary>
		/// A spammy piece of content
		/// </summary>
		[EnumMember(Value = "SPAM")]
		Spam,

		/// <summary>
		/// An abusive or harassing piece of content
		/// </summary>
		[EnumMember(Value = "ABUSE")]
		Abuse,

		/// <summary>
		/// An irrelevant piece of content
		/// </summary>
		[EnumMember(Value = "OFF_TOPIC")]
		OffTopic,

		/// <summary>
		/// An outdated piece of content
		/// </summary>
		[EnumMember(Value = "OUTDATED")]
		Outdated,

		/// <summary>
		/// A duplicated piece of content
		/// </summary>
		[EnumMember(Value = "DUPLICATE")]
		Duplicate,

		/// <summary>
		/// The content has been resolved
		/// </summary>
		[EnumMember(Value = "RESOLVED")]
		Resolved,
	}
}
