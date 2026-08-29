using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Software or company that hosts social media accounts.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SocialAccountProvider
    {
        /// <summary>
        /// Catch-all for social media providers that do not yet have specific handling.
        /// </summary>
        [EnumMember(Value = "GENERIC")]
        Generic,

        /// <summary>
        /// Social media and networking website.
        /// </summary>
        [EnumMember(Value = "FACEBOOK")]
        Facebook,

        /// <summary>
        /// Fork of Mastodon with a greater focus on local posting.
        /// </summary>
        [EnumMember(Value = "HOMETOWN")]
        Hometown,

        /// <summary>
        /// Social media website with a focus on photo and video sharing.
        /// </summary>
        [EnumMember(Value = "INSTAGRAM")]
        Instagram,

        /// <summary>
        /// Professional networking website.
        /// </summary>
        [EnumMember(Value = "LINKEDIN")]
        Linkedin,

        /// <summary>
        /// Open-source federated microblogging service.
        /// </summary>
        [EnumMember(Value = "MASTODON")]
        Mastodon,

        /// <summary>
        /// Social news aggregation and discussion website.
        /// </summary>
        [EnumMember(Value = "REDDIT")]
        Reddit,

        /// <summary>
        /// Live-streaming service.
        /// </summary>
        [EnumMember(Value = "TWITCH")]
        Twitch,

        /// <summary>
        /// Microblogging website.
        /// </summary>
        [EnumMember(Value = "TWITTER")]
        Twitter,

        /// <summary>
        /// Online video platform.
        /// </summary>
        [EnumMember(Value = "YOUTUBE")]
        Youtube,

        /// <summary>
        /// JavaScript package registry.
        /// </summary>
        [EnumMember(Value = "NPM")]
        Npm,
    }
}