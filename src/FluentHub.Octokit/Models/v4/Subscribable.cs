namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Entities that can be subscribed to for web and email notifications.
    /// </summary>
    public interface ISubscribable
    {
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

namespace FluentHub.Octokit.Models.v4.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubISubscribable
    {
        

        public ID Id { get; set; }

        public bool ViewerCanSubscribe { get; set; }

        public SubscriptionState? ViewerSubscription { get; set; }
    }
}