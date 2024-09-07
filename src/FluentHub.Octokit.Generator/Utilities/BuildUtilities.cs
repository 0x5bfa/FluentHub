// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Octokit.ModelGenerator.Models;
using Octokit.GraphQL.Core.Introspection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentHub.Octokit.ModelGenerator.Utilities
{
	internal static class BuildUtilities
	{
		private static readonly string[] pagingFields = new[] { "first", "after", "last", "before" };

		public static IEnumerable<InputValueModel> SortArgs(IEnumerable<InputValueModel> args)
		{
			return args.OrderBy(x => x, new DefaultValueComparer());
		}

		private class DefaultValueComparer : IComparer<InputValueModel>
		{
			public int Compare(InputValueModel x, InputValueModel y)
			{
				var defaultValueWeightX = DefaultValueWeight(x);
				var defaultValueWeightY = DefaultValueWeight(y);
				var pagingX = Array.IndexOf(pagingFields, x.Name);
				var pagingY = Array.IndexOf(pagingFields, y.Name);

				if (defaultValueWeightX != defaultValueWeightY)
				{
					return DefaultValueWeight(x) - DefaultValueWeight(y);
				}
				else if (pagingX != pagingY)
				{
					if (pagingX == -1) pagingX = int.MaxValue;
					if (pagingY == -1) pagingY = int.MaxValue;
					return pagingX - pagingY;
				}
				else
				{
					return StringComparer.OrdinalIgnoreCase.Compare(x.Name, y.Name);
				}
			}

			private static int DefaultValueWeight(InputValueModel i)
			{
				return i.DefaultValue != null || i.Type.Kind != TypeKind.NonNull ? 1 : -1;
			}
		}
	}
}
