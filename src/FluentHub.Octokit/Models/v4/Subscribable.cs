// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// Entities that can be subscribed to for web and email notifications.
	/// </summary>
	public interface ISubscribable
	{
		/// <summary>
		/// The Node ID of the Subscribable object
		/// </summary>
		ID Id { get; set; }

		/// <summary>
		/// Check if the viewer is able to change their subscription status for the repository.
		/// </summary>
		bool ViewerCanSubscribe { get; set; }

		/// <summary>
		/// Identifies if the viewer is watching, not watching, or ignoring the subscribable entity.
		/// </summary>
		SubscriptionState? ViewerSubscription { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class Subscribable : ISubscribable
	{
		public ID Id { get; set; }

		public bool ViewerCanSubscribe { get; set; }

		public SubscriptionState? ViewerSubscription { get; set; }
	}
}

