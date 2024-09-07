// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Octokit.ModelGenerator.Models;
using FluentHub.Octokit.ModelGenerator.Utilities;
using Octokit.GraphQL.Core.Utilities;
using System;
using System.Text;

namespace FluentHub.Octokit.ModelGenerator.Generators
{
	public static class EnumGenerator
	{
		public static string Generate(TypeModel type, string entityNamespace)
		{
			var enumName = TypeUtilities.GetClassName(type);

			var licenseNotice = @"// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.";

			return $@"{licenseNotice}

namespace {entityNamespace}
{{
	{GenerateDocComments(type)}[JsonConverter(typeof(StringEnumConverter))]
	public enum {enumName}
	{{{GenerateEnumValues(type)}	}}
}}
";
		}

		private static string GenerateDocComments(TypeModel type)
		{
			if (!string.IsNullOrWhiteSpace(type.Description))
			{
				var builder = new StringBuilder();
				DocCommentGenerator.GenerateSummary(type.Description, 4, builder);
				builder.Append("	");
				return builder.ToString().TrimStart();
			}
			else
			{
				return null;
			}
		}

		private static string GenerateDocComments(EnumValueModel value)
		{
			if (!string.IsNullOrWhiteSpace(value.Description))
			{
				var builder = new StringBuilder();
				DocCommentGenerator.GenerateSummary(value.Description, 8, builder);
				return builder.ToString();
			}
			else
			{
				return null;
			}
		}

		private static string GenerateEnumValues(TypeModel type)
		{
			var builder = new StringBuilder();

			if (type.EnumValues?.Count > 0)
			{
				foreach (var value in type.EnumValues)
				{
					builder.AppendLine();
					builder.Append(GenerateDocComments(value));
					builder.AppendLine(GenerateEnumValue(value));
				}
			}
			else
			{
				builder.AppendLine();
			}

			return builder.ToString();
		}

		private static string GenerateEnumValue(EnumValueModel value)
		{
			var obsoleteAttribute = value.IsDeprecated
					? $@"		{AttributeGenerator.GenerateObsoleteAttribute(value.DeprecationReason)}{Environment.NewLine}"
					: string.Empty;

			return $@"{obsoleteAttribute}		[EnumMember(Value = ""{value.Name}"")]
		{value.Name.SnakeCaseToPascalCase()},";
		}
	}
}
