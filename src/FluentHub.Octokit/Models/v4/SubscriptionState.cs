// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible states of a subscription.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SubscriptionState
	{
		/// <summary>
		/// The User is only notified when participating or @mentioned.
		/// </summary>
		[EnumMember(Value = "UNSUBSCRIBED")]
		Unsubscribed,

		/// <summary>
		/// The User is notified of all conversations.
		/// </summary>
		[EnumMember(Value = "SUBSCRIBED")]
		Subscribed,

		/// <summary>
		/// The User is never notified.
		/// </summary>
		[EnumMember(Value = "IGNORED")]
		Ignored,
	}
}
