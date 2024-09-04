// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using Octokit.GraphQL.Core.Introspection;
using System.Collections.Generic;

namespace FluentHub.Octokit.ModelGenerator.Models
{
	public class DirectiveModel
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public IList<DirectiveLocation> Locations { get; set; }

		public IList<InputValueModel> Args { get; set; }
	}
}
