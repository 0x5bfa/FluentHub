// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible GitHub Enterprise deployments where this user can exist.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum EnterpriseUserDeployment
	{
		/// <summary>
		/// The user is part of a GitHub Enterprise Cloud deployment.
		/// </summary>
		[EnumMember(Value = "CLOUD")]
		Cloud,

		/// <summary>
		/// The user is part of a GitHub Enterprise Server deployment.
		/// </summary>
		[EnumMember(Value = "SERVER")]
		Server,
	}
}
