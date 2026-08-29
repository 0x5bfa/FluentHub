using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible errors that will prevent a user from updating a comment.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CommentCannotUpdateReason
    {
        /// <summary>
        /// Unable to create comment because repository is archived.
        /// </summary>
        [EnumMember(Value = "ARCHIVED")]
        Archived,

        /// <summary>
        /// You must be the author or have write access to this repository to update this comment.
        /// </summary>
        [EnumMember(Value = "INSUFFICIENT_ACCESS")]
        InsufficientAccess,

        /// <summary>
        /// Unable to create comment because issue is locked.
        /// </summary>
        [EnumMember(Value = "LOCKED")]
        Locked,

        /// <summary>
        /// You must be logged in to update this comment.
        /// </summary>
        [EnumMember(Value = "LOGIN_REQUIRED")]
        LoginRequired,

        /// <summary>
        /// Repository is under maintenance.
        /// </summary>
        [EnumMember(Value = "MAINTENANCE")]
        Maintenance,

        /// <summary>
        /// At least one email address must be verified to update this comment.
        /// </summary>
        [EnumMember(Value = "VERIFIED_EMAIL_REQUIRED")]
        VerifiedEmailRequired,

        /// <summary>
        /// You cannot update this comment
        /// </summary>
        [EnumMember(Value = "DENIED")]
        Denied,
    }
}