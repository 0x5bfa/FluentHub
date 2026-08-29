using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible reasons a given repository could be in a locked state.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepositoryLockReason
    {
        /// <summary>
        /// The repository is locked due to a move.
        /// </summary>
        [EnumMember(Value = "MOVING")]
        Moving,

        /// <summary>
        /// The repository is locked due to a billing related reason.
        /// </summary>
        [EnumMember(Value = "BILLING")]
        Billing,

        /// <summary>
        /// The repository is locked due to a rename.
        /// </summary>
        [EnumMember(Value = "RENAME")]
        Rename,

        /// <summary>
        /// The repository is locked due to a migration.
        /// </summary>
        [EnumMember(Value = "MIGRATING")]
        Migrating,

        /// <summary>
        /// The repository is locked due to a trade controls related reason.
        /// </summary>
        [EnumMember(Value = "TRADE_RESTRICTION")]
        TradeRestriction,

        /// <summary>
        /// The repository is locked due to an ownership transfer.
        /// </summary>
        [EnumMember(Value = "TRANSFERRING_OWNERSHIP")]
        TransferringOwnership,
    }
}