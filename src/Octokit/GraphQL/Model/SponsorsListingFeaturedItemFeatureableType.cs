using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The different kinds of records that can be featured on a GitHub Sponsors profile page.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SponsorsListingFeaturedItemFeatureableType
    {
        /// <summary>
        /// A repository owned by the user or organization with the GitHub Sponsors profile.
        /// </summary>
        [EnumMember(Value = "REPOSITORY")]
        Repository,

        /// <summary>
        /// A user who belongs to the organization with the GitHub Sponsors profile.
        /// </summary>
        [EnumMember(Value = "USER")]
        User,
    }
}