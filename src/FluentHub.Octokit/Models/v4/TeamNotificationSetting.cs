// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible team notification values.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TeamNotificationSetting
	{
		/// <summary>
		/// Everyone will receive notifications when the team is @mentioned.
		/// </summary>
		[EnumMember(Value = "NOTIFICATIONS_ENABLED")]
		NotificationsEnabled,

		/// <summary>
		/// No one will receive notifications.
		/// </summary>
		[EnumMember(Value = "NOTIFICATIONS_DISABLED")]
		NotificationsDisabled,
	}
}
