// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible funding platforms for repository funding links.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum FundingPlatform
	{
		/// <summary>
		/// GitHub funding platform.
		/// </summary>
		[EnumMember(Value = "GITHUB")]
		Github,

		/// <summary>
		/// Patreon funding platform.
		/// </summary>
		[EnumMember(Value = "PATREON")]
		Patreon,

		/// <summary>
		/// Open Collective funding platform.
		/// </summary>
		[EnumMember(Value = "OPEN_COLLECTIVE")]
		OpenCollective,

		/// <summary>
		/// Ko-fi funding platform.
		/// </summary>
		[EnumMember(Value = "KO_FI")]
		KoFi,

		/// <summary>
		/// Tidelift funding platform.
		/// </summary>
		[EnumMember(Value = "TIDELIFT")]
		Tidelift,

		/// <summary>
		/// Community Bridge funding platform.
		/// </summary>
		[EnumMember(Value = "COMMUNITY_BRIDGE")]
		CommunityBridge,

		/// <summary>
		/// Liberapay funding platform.
		/// </summary>
		[EnumMember(Value = "LIBERAPAY")]
		Liberapay,

		/// <summary>
		/// IssueHunt funding platform.
		/// </summary>
		[EnumMember(Value = "ISSUEHUNT")]
		Issuehunt,

		/// <summary>
		/// Otechie funding platform.
		/// </summary>
		[EnumMember(Value = "OTECHIE")]
		Otechie,

		/// <summary>
		/// LFX Crowdfunding funding platform.
		/// </summary>
		[EnumMember(Value = "LFX_CROWDFUNDING")]
		LfxCrowdfunding,

		/// <summary>
		/// Custom funding platform.
		/// </summary>
		[EnumMember(Value = "CUSTOM")]
		Custom,
	}
}
