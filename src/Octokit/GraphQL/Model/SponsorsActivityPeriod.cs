using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible time periods for which Sponsors activities can be requested.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SponsorsActivityPeriod
    {
        /// <summary>
        /// The previous calendar day.
        /// </summary>
        [EnumMember(Value = "DAY")]
        Day,

        /// <summary>
        /// The previous seven days.
        /// </summary>
        [EnumMember(Value = "WEEK")]
        Week,

        /// <summary>
        /// The previous thirty days.
        /// </summary>
        [EnumMember(Value = "MONTH")]
        Month,

        /// <summary>
        /// Don't restrict the activity to any date range, include all activity.
        /// </summary>
        [EnumMember(Value = "ALL")]
        All,
    }
}