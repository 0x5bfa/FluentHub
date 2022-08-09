using System;
using System.Text;
using FluentHub.Octokit.Generation.Models;
using FluentHub.Octokit.Generation.Utilities;
using Octokit.GraphQL.Core.Introspection;

namespace FluentHub.Octokit.Generation
{
    internal static class InputObjectGenerator
    {
        public static string Generate(TypeModel type, string entityNamespace)
        {
            var className = TypeUtilities.GetClassName(type);

            return $@"namespace {entityNamespace}
{{
    using System;
    using System.Collections.Generic;

    {GenerateDocComments(type)}public class {className}
    {{{GenerateFields(type)}
    }}
}}";
        }

        private static string GenerateFields(TypeModel type)
        {
            var builder = new StringBuilder();

            if (type.InputFields?.Count > 0)
            {
                var first = true;
                builder.AppendLine();

                foreach (var field in type.InputFields)
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

        private static string GenerateField(InputValueModel field)
        {
            var result = GenerateDocComments(field);
            var name = TypeUtilities.PascalCase(field.Name);
            var typeName = TypeUtilities.GetCSharpArgType(field.Type);
            return result + $"        public {typeName} {name} {{ get; set; }}";
        }

        private static string GenerateDocComments(TypeModel type)
        {
            if (!string.IsNullOrWhiteSpace(type.Description))
            {
                var builder = new StringBuilder();
                DocCommentGenerator.GenerateSummary(type.Description, 4, builder);
                builder.Append("    ");
                return builder.ToString().TrimStart();
            }
            else
            {
                return null;
            }
        }

        private static string GenerateDocComments(InputValueModel field)
        {
            if (!string.IsNullOrWhiteSpace(field.Description))
            {
                var builder = new StringBuilder();
                DocCommentGenerator.GenerateSummary(field.Description, 8, builder);
                return builder.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
