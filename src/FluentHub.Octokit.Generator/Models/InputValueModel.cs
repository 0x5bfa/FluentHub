// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.ModelGenerator.Models
{
	public class InputValueModel
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public TypeModel Type { get; set; }

		public string DefaultValue { get; set; }
	}
}
