using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
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
        [Obsolete(@"Suggested topics are no longer supported Removal on 2024-04-01 UTC.")]
        [EnumMember(Value = "NOT_RELEVANT")]
        NotRelevant,

        /// <summary>
        /// The suggested topic is too specific for the repository (e.g. #ruby-on-rails-version-4-2-1).
        /// </summary>
        [Obsolete(@"Suggested topics are no longer supported Removal on 2024-04-01 UTC.")]
        [EnumMember(Value = "TOO_SPECIFIC")]
        TooSpecific,

        /// <summary>
        /// The viewer does not like the suggested topic.
        /// </summary>
        [Obsolete(@"Suggested topics are no longer supported Removal on 2024-04-01 UTC.")]
        [EnumMember(Value = "PERSONAL_PREFERENCE")]
        PersonalPreference,

        /// <summary>
        /// The suggested topic is too general for the repository.
        /// </summary>
        [Obsolete(@"Suggested topics are no longer supported Removal on 2024-04-01 UTC.")]
        [EnumMember(Value = "TOO_GENERAL")]
        TooGeneral,
    }
}