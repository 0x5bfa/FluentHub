// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.ModelGenerator.Models
{
	public class EnumValueModel
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public bool IsDeprecated { get; set; }

		public string DeprecationReason { get; set; }
	}
}
