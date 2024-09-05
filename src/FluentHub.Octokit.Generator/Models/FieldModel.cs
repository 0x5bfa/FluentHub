// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using System.Collections.Generic;

namespace FluentHub.Octokit.ModelGenerator.Models
{
	public class FieldModel
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public IList<InputValueModel> Args { get; set; }

		public TypeModel Type { get; set; }

		public bool IsDeprecated { get; set; }

		public string DeprecationReason { get; set; }
	}
}
