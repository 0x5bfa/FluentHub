// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// Entities that can be subscribed to for web and email notifications.
	/// </summary>
	public interface ISubscribableThread
	{
		ID Id { get; set; }

		/// <summary>
		/// Identifies the viewer's thread subscription form action.
		/// </summary>
		ThreadSubscriptionFormAction? ViewerThreadSubscriptionFormAction { get; set; }

		/// <summary>
		/// Identifies the viewer's thread subscription status.
		/// </summary>
		ThreadSubscriptionState? ViewerThreadSubscriptionStatus { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class SubscribableThread : ISubscribableThread
	{
		public ID Id { get; set; }

		public ThreadSubscriptionFormAction? ViewerThreadSubscriptionFormAction { get; set; }

		public ThreadSubscriptionState? ViewerThreadSubscriptionStatus { get; set; }
	}
}

