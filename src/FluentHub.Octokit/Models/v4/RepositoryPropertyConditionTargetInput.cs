// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Parameters to be used for the repository_property condition
	/// </summary>
	public class RepositoryPropertyConditionTargetInput
	{
		/// <summary>
		/// Array of repository properties that must not match.
		/// </summary>
		public List<PropertyTargetDefinitionInput> Exclude { get; set; }

		/// <summary>
		/// Array of repository properties that must match
		/// </summary>
		public List<PropertyTargetDefinitionInput> Include { get; set; }
	}
}
