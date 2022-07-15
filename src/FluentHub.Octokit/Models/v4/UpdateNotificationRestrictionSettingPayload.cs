namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Autogenerated return type of UpdateNotificationRestrictionSetting
    /// </summary>
    public class UpdateNotificationRestrictionSettingPayload
    {
        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }

        /// <summary>
        /// The owner on which the setting was updated.
        /// </summary>
        public VerifiableDomainOwner Owner { get; set; }
    }
}