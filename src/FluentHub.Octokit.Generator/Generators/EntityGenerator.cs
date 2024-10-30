// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Octokit.ModelGenerator.Models;
using FluentHub.Octokit.ModelGenerator.Utilities;
using Octokit.GraphQL.Core.Introspection;
using System;
using System.Linq;
using System.Text;

namespace FluentHub.Octokit.ModelGenerator.Generators
{
	internal static class EntityGenerator
	{
		public static string Generate(
			TypeModel type,
			string rootNamespace,
			string queryType,
			string modifiers = "public ",
			bool generateDocComments = true,
			string entityNamespace = null,
			bool isStub = false)
		{
			var className = TypeUtilities.GetClassName(type);
			var pagingConnectionNodeType = GetPagingConnectionNodeType(type);

			string licenseNotice = string.Empty;

			if (!isStub)
			{
				licenseNotice = @"// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

";
			}

			return $@"{licenseNotice}namespace {rootNamespace}
{{
	{GenerateDocComments(type, generateDocComments)}{modifiers}class {className}{GenerateImplementedInterfaces(type, pagingConnectionNodeType)}
	{{{GenerateFields(type, generateDocComments, rootNamespace, entityNamespace, queryType, pagingConnectionNodeType != null)}
	}}
}}
";
		}

		public static string GenerateRoot(TypeModel type, string rootNamespace, string entityNamespace, string interfaceName, string queryType)
		{
			var className = TypeUtilities.GetClassName(type);

			var includeEntities = rootNamespace == entityNamespace ? string.Empty : $@"
	using {entityNamespace};";

			var licenseNotice = @"// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.";

			return $@"{licenseNotice}

namespace {rootNamespace}
{{
	{GenerateDocComments(type, true)}public class {className} : {interfaceName}
	{{{GenerateFields(type, true, rootNamespace, entityNamespace, queryType, false)}
	}}
}}
";
		}

		private static string GenerateFields(TypeModel type, bool generateDocComments, string rootNamespace, string entityNamespace, string queryType, bool isPagingConnection)
		{
			var builder = new StringBuilder();
			var first = true;

			if (type.Fields?.Count > 0)
			{
				foreach (var field in type.Fields)
				{
					if (!first)
					{
						builder.AppendLine();
					}

					builder.AppendLine();
					builder.Append(GenerateField(field, generateDocComments, rootNamespace, entityNamespace, queryType));

					first = false;
				}
			}

			return builder.ToString();
		}

		private static string GenerateField(FieldModel field, bool generateDocComments, string rootNamespace, string entityNamespace, string queryType)
		{
			var method = field.Args?.Count > 0;
			var result = GenerateDocComments(field, generateDocComments);
			var reduced = TypeUtilities.ReduceType(field.Type);

			if (TypeUtilities.IsCSharpPrimitive(reduced))
			{
				result += method ?
					GenerateScalarMethod(field, reduced) :
					GenerateScalarField(field, reduced, generateDocComments);
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
					GenerateObjectMethod(field, reduced, entityNamespace) :
					GenerateObjectField(field, reduced, rootNamespace, entityNamespace, queryType);
			}

			return result;
		}

		private static string GenerateDocComments(TypeModel type, bool generate)
		{
			if (generate && !string.IsNullOrWhiteSpace(type.Description))
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

		private static string GenerateDocComments(FieldModel field, bool generate)
		{
			if (generate && !string.IsNullOrWhiteSpace(field.Description))
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

		private static void GenerateDocComments(string text, int indentation, StringBuilder builder)
		{
			var indent = new string(' ', indentation);

			foreach (var line in text.Split('\r', '\n').Where(l => !string.IsNullOrWhiteSpace(l)))
			{
				builder.Append(indent);
				builder.Append("/// ");
				builder.AppendLine(line);
			}
		}

		private static string GenerateScalarField(FieldModel field, TypeModel type, bool generateDocComments = true)
		{
			var obsoleteAttribute = field.IsDeprecated
				? $@"		{AttributeGenerator.GenerateObsoleteAttribute(field.DeprecationReason)}{Environment.NewLine}"
				: string.Empty;

			var name = TypeUtilities.PascalCase(field.Name);
			var typeName = TypeUtilities.GetCSharpReturnType(type);

			string result = $"{obsoleteAttribute}		public {typeName} {name} {{ get; set; }}";

			if (TypeUtilities.GetCSharpReturnType(type) == "DateTimeOffset"
				|| TypeUtilities.GetCSharpReturnType(type) == "DateTimeOffset?")
			{
				result += "\r\n\r\n";
				if (generateDocComments)
				{
					result += $"		/// <summary>\r\n";
					result += $"		/// Humanized string of \"{field.Description}\"\r\n";
					result += $"		/// <summary>\r\n";
				}
				result += $"		public string {name}Humanized {{ get; set; }}";
			}

			return result;
		}

		private static string GenerateObjectField(FieldModel field, TypeModel type, string rootNamespace, string entityNamespace, string queryType)
		{
			var obsoleteAttribute = field.IsDeprecated
				? $@"		{AttributeGenerator.GenerateObsoleteAttribute(field.DeprecationReason)}{Environment.NewLine}"
				: string.Empty;

			var name = TypeUtilities.PascalCase(field.Name);
			var typeName = TypeUtilities.GetCSharpReturnType(type);

			return $"{obsoleteAttribute}		public {typeName} {name} {{ get; set; }}";
		}

		private static string GenerateScalarMethod(FieldModel field, TypeModel type)
		{
			var obsoleteAttribute = field.IsDeprecated
				? $@"		{AttributeGenerator.GenerateObsoleteAttribute(field.DeprecationReason)}{Environment.NewLine}"
				: string.Empty;

			var name = TypeUtilities.PascalCase(field.Name);
			var csharpType = TypeUtilities.GetCSharpReturnType(type);

			return $"{obsoleteAttribute}		public {csharpType} {name} {{ get; set; }}";
		}

		private static string GenerateObjectMethod(FieldModel field, TypeModel type, string entityNamespace)
		{
			var name = TypeUtilities.PascalCase(field.Name);
			var typeName = TypeUtilities.GetCSharpReturnType(type);

			return $"		public {typeName} {name} {{ get; set; }}";
		}

		private static string GenerateListField(FieldModel field, TypeModel type)
		{
			var obsoleteAttribute = field.IsDeprecated
				? $@"		{AttributeGenerator.GenerateObsoleteAttribute(field.DeprecationReason)}{Environment.NewLine}"
				: string.Empty;

			var name = TypeUtilities.PascalCase(field.Name);
			var typeName = TypeUtilities.GetCSharpReturnType(type);

			return $"{obsoleteAttribute}		public {typeName} {name} {{ get; set; }}";
		}

		private static string GenerateListMethod(FieldModel field, TypeModel type)
		{
			var name = TypeUtilities.PascalCase(field.Name);
			var typeName = TypeUtilities.GetCSharpReturnType(type);

			return $"		public {typeName} {name} {{ get; set; }}";
		}

		private static string GenerateImplementedInterfaces(TypeModel type, TypeModel pagingConnectionNodeType)
		{
			int numOfInterface = 0;

			var builder = new StringBuilder();

			if (type.Interfaces != null)
			{
				foreach (var iface in type.Interfaces)
				{
					if (numOfInterface == 0)
					{
						builder.Append(" : ");
					}
					else
					{
						builder.Append(", ");
					}

					builder.Append(TypeUtilities.GetInterfaceName(iface));
					numOfInterface++;
				}
			}

			return builder.ToString();
		}

		private static TypeModel GetPagingConnectionNodeType(TypeModel type)
		{
			var nodes = type.Fields?.FirstOrDefault(x =>
				x.Name == "nodes" &&
				x.Type.Kind == TypeKind.List);

			var pageInfo = type.Fields?.FirstOrDefault(x =>
				x.Name == "pageInfo" &&
				x.Type.Kind == TypeKind.NonNull &&
				x.Type.OfType.Kind == TypeKind.Object &&
				x.Type.OfType.Name == "PageInfo");

			if (nodes != null && pageInfo != null)
				return nodes.Type.OfType;

			return null;
		}

		private static string GetEntityImplementationName(TypeModel type, string entityNamespace)
		{
			switch (type.Kind)
			{
				case TypeKind.Interface:
					return entityNamespace + ".Internal.Stub" + TypeUtilities.GetInterfaceName(type);
				default:
					return entityNamespace + "." + TypeUtilities.GetClassName(type);
			}
		}
	}
}
