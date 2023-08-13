// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Choose which environments must be successfully deployed to before branches can be merged into a branch that matches this rule.
	/// </summary>
	public class RequiredDeploymentsParameters
	{
		/// <summary>
		/// The environments that must be successfully deployed to before branches can be merged.
		/// </summary>
		public List<string> RequiredDeploymentEnvironments { get; set; }
	}
}
