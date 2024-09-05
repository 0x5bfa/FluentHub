// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Octokit.ModelGenerator.Models;
using FluentHub.Octokit.ModelGenerator.Utilities;
using Octokit.GraphQL.Core.Introspection;
using System;
using System.Text;

namespace FluentHub.Octokit.ModelGenerator.Generators
{
	internal static class InterfaceGenerator
	{
		public static string Generate(TypeModel type, string entityNamespace, string queryType)
		{
			var className = TypeUtilities.GetInterfaceName(type);

			var licenseNotice = @"// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.";

			return $@"{licenseNotice}

namespace {entityNamespace}
{{

	{GenerateDocComments(type)}public interface {className}
	{{{GenerateFields(type)}
	}}
}}

{GenerateStub(type, entityNamespace, queryType)}
";
		}

		private static string GenerateFields(TypeModel type)
		{
			var builder = new StringBuilder();

			if (type.Fields?.Count > 0)
			{
				var first = true;
				builder.AppendLine();

				foreach (var field in type.Fields)
				{
					if (!first)
					{
						builder.AppendLine();
						builder.AppendLine();
					}

					builder.Append(GenerateField(field));

					first = false;
				}
			}

			return builder.ToString();
		}

		private static string GenerateField(FieldModel field)
		{
			var method = field.Args?.Count > 0;
			var result = GenerateDocComments(field);
			var reduced = TypeUtilities.ReduceType(field.Type);

			if (TypeUtilities.IsCSharpPrimitive(reduced))
			{
				result += method ?
					GenerateScalarMethod(field, reduced) :
					GenerateScalarField(field, reduced);
			}
			else if (reduced.Kind == TypeKind.List)
			{
				result += method ?
					GenerateListMethod(field, reduced) :
					GenerateListField(field, reduced);
			}
			else
			{
				result += method ?
					GenerateObjectMethod(field, reduced) :
					GenerateObjectField(field, reduced);
			}

			return result;
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

		private static string GenerateDocComments(FieldModel field)
		{
			if (!string.IsNullOrWhiteSpace(field.Description))
			{
				var builder = new StringBuilder();
				DocCommentGenerator.GenerateSummary(field.Description, 8, builder);

				if (field.Args != null)
				{
					foreach (var arg in BuildUtilities.SortArgs(field.Args))
					{
						if (!string.IsNullOrWhiteSpace(arg.Description))
						{
							var description = string.Join(" ", arg.Description.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)).Trim();
							builder.AppendLine($"		/// <param name=\"{arg.Name}\">{description}</param>");
						}
					}
				}

				return builder.ToString();
			}
			else
			{
				return null;
			}
		}

		private static string GenerateScalarField(FieldModel field, TypeModel type)
		{
			var name = TypeUtilities.PascalCase(field.Name);
			string result = $"		{TypeUtilities.GetCSharpReturnType(type)} {name} {{ get; set; }}";

			if (TypeUtilities.GetCSharpReturnType(type) == "DateTimeOffset"
				|| TypeUtilities.GetCSharpReturnType(type) == "DateTimeOffset?")
			{
				result += "\r\n\r\n";
				result += $"		/// <summary>\r\n";
				result += $"		/// Humanized string of \"{field.Description}\"\r\n";
				result += $"		/// <summary>\r\n";
				result += $"		string {name}Humanized {{ get; set; }}";
			}

			return result;
		}

		private static string GenerateObjectField(FieldModel field, TypeModel type)
		{
			var name = TypeUtilities.PascalCase(field.Name);
			var typeName = TypeUtilities.GetCSharpReturnType(type);
			return $"		{typeName} {name} {{ get; set; }}";
		}

		private static string GenerateScalarMethod(FieldModel field, TypeModel type)
		{
			var name = TypeUtilities.PascalCase(field.Name);
			var csharpType = TypeUtilities.GetCSharpReturnType(type);

			return $"		{csharpType} {name} {{ get; set; }}";
		}

		private static string GenerateObjectMethod(FieldModel field, TypeModel type)
		{
			var name = TypeUtilities.PascalCase(field.Name);
			var typeName = TypeUtilities.GetCSharpReturnType(type);

			return $"		{typeName} {name} {{ get; set; }}";
		}

		private static string GenerateListField(FieldModel field, TypeModel type)
		{
			var name = TypeUtilities.PascalCase(field.Name);
			var typeName = TypeUtilities.GetCSharpReturnType(type);

			return $"		{typeName} {name} {{ get; set; }}";
		}

		private static string GenerateListMethod(FieldModel field, TypeModel type)
		{
			var name = TypeUtilities.PascalCase(field.Name);
			var typeName = TypeUtilities.GetCSharpReturnType(type);

			return $"		{typeName} {name} {{ get; set; }}";
		}

		private static string GenerateStub(TypeModel type, string entityNamespace, string queryType)
		{
			var stubType = type.Clone();
			stubType.Name = TypeUtilities.GetInterfaceName(type).TrimStart('I'); // impl
			stubType.Kind = TypeKind.Object;
			stubType.Interfaces = new[] { type };

			return EntityGenerator.Generate(stubType, entityNamespace, queryType, entityNamespace: entityNamespace, modifiers: "public ", generateDocComments: false, isStub: true);
		}
	}
}
