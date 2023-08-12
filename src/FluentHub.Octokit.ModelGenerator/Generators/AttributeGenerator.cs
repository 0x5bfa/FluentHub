// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.ModelGenerator.Generators
{
	internal static class AttributeGenerator
	{
		public static string GenerateObsoleteAttribute(string reason)
		{
			if (string.IsNullOrWhiteSpace(reason))
				return "[Obsolete]";

			return $"[Obsolete(@\"{reason}\")]";
		}
	}
}
