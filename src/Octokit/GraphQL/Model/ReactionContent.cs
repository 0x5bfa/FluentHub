using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Emojis that can be attached to Issues, Pull Requests and Comments.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ReactionContent
    {
        /// <summary>
        /// Represents the `:+1:` emoji.
        /// </summary>
        [EnumMember(Value = "THUMBS_UP")]
        ThumbsUp,

        /// <summary>
        /// Represents the `:-1:` emoji.
        /// </summary>
        [EnumMember(Value = "THUMBS_DOWN")]
        ThumbsDown,

        /// <summary>
        /// Represents the `:laugh:` emoji.
        /// </summary>
        [EnumMember(Value = "LAUGH")]
        Laugh,

        /// <summary>
        /// Represents the `:hooray:` emoji.
        /// </summary>
        [EnumMember(Value = "HOORAY")]
        Hooray,

        /// <summary>
        /// Represents the `:confused:` emoji.
        /// </summary>
        [EnumMember(Value = "CONFUSED")]
        Confused,

        /// <summary>
        /// Represents the `:heart:` emoji.
        /// </summary>
        [EnumMember(Value = "HEART")]
        Heart,

        /// <summary>
        /// Represents the `:rocket:` emoji.
        /// </summary>
        [EnumMember(Value = "ROCKET")]
        Rocket,

        /// <summary>
        /// Represents the `:eyes:` emoji.
        /// </summary>
        [EnumMember(Value = "EYES")]
        Eyes,
    }
}