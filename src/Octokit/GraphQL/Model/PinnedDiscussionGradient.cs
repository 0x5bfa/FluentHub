using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Preconfigured gradients that may be used to style discussions pinned within a repository.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PinnedDiscussionGradient
    {
        /// <summary>
        /// A gradient of red to orange
        /// </summary>
        [EnumMember(Value = "RED_ORANGE")]
        RedOrange,

        /// <summary>
        /// A gradient of blue to mint
        /// </summary>
        [EnumMember(Value = "BLUE_MINT")]
        BlueMint,

        /// <summary>
        /// A gradient of blue to purple
        /// </summary>
        [EnumMember(Value = "BLUE_PURPLE")]
        BluePurple,

        /// <summary>
        /// A gradient of pink to blue
        /// </summary>
        [EnumMember(Value = "PINK_BLUE")]
        PinkBlue,

        /// <summary>
        /// A gradient of purple to coral
        /// </summary>
        [EnumMember(Value = "PURPLE_CORAL")]
        PurpleCoral,
    }
}