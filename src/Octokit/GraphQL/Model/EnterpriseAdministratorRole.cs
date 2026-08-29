using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible administrator roles in an enterprise account.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnterpriseAdministratorRole
    {
        /// <summary>
        /// Represents an owner of the enterprise account.
        /// </summary>
        [EnumMember(Value = "OWNER")]
        Owner,

        /// <summary>
        /// Represents a billing manager of the enterprise account.
        /// </summary>
        [EnumMember(Value = "BILLING_MANAGER")]
        BillingManager,
    }
}