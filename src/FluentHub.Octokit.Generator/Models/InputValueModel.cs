// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using System;

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
