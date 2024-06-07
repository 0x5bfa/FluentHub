// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A property that must match
	/// </summary>
	public class PropertyTargetDefinition
	{
		/// <summary>
		/// The name of the property
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The values to match for
		/// </summary>
		public List<string> PropertyValues { get; set; }
	}
}
