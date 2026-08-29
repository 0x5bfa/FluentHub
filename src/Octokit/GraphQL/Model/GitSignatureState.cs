using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The state of a Git signature.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GitSignatureState
    {
        /// <summary>
        /// Valid signature and verified by GitHub
        /// </summary>
        [EnumMember(Value = "VALID")]
        Valid,

        /// <summary>
        /// Invalid signature
        /// </summary>
        [EnumMember(Value = "INVALID")]
        Invalid,

        /// <summary>
        /// Malformed signature
        /// </summary>
        [EnumMember(Value = "MALFORMED_SIG")]
        MalformedSig,

        /// <summary>
        /// Key used for signing not known to GitHub
        /// </summary>
        [EnumMember(Value = "UNKNOWN_KEY")]
        UnknownKey,

        /// <summary>
        /// Invalid email used for signing
        /// </summary>
        [EnumMember(Value = "BAD_EMAIL")]
        BadEmail,

        /// <summary>
        /// Email used for signing unverified on GitHub
        /// </summary>
        [EnumMember(Value = "UNVERIFIED_EMAIL")]
        UnverifiedEmail,

        /// <summary>
        /// Email used for signing not known to GitHub
        /// </summary>
        [EnumMember(Value = "NO_USER")]
        NoUser,

        /// <summary>
        /// Unknown signature type
        /// </summary>
        [EnumMember(Value = "UNKNOWN_SIG_TYPE")]
        UnknownSigType,

        /// <summary>
        /// Unsigned
        /// </summary>
        [EnumMember(Value = "UNSIGNED")]
        Unsigned,

        /// <summary>
        /// Internal error - the GPG verification service is unavailable at the moment
        /// </summary>
        [EnumMember(Value = "GPGVERIFY_UNAVAILABLE")]
        GpgverifyUnavailable,

        /// <summary>
        /// Internal error - the GPG verification service misbehaved
        /// </summary>
        [EnumMember(Value = "GPGVERIFY_ERROR")]
        GpgverifyError,

        /// <summary>
        /// The usage flags for the key that signed this don't allow signing
        /// </summary>
        [EnumMember(Value = "NOT_SIGNING_KEY")]
        NotSigningKey,

        /// <summary>
        /// Signing key expired
        /// </summary>
        [EnumMember(Value = "EXPIRED_KEY")]
        ExpiredKey,

        /// <summary>
        /// Valid signature, pending certificate revocation checking
        /// </summary>
        [EnumMember(Value = "OCSP_PENDING")]
        OcspPending,

        /// <summary>
        /// Valid signature, though certificate revocation check failed
        /// </summary>
        [EnumMember(Value = "OCSP_ERROR")]
        OcspError,

        /// <summary>
        /// The signing certificate or its chain could not be verified
        /// </summary>
        [EnumMember(Value = "BAD_CERT")]
        BadCert,

        /// <summary>
        /// One or more certificates in chain has been revoked
        /// </summary>
        [EnumMember(Value = "OCSP_REVOKED")]
        OcspRevoked,
    }
}