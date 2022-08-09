using System;
using System.Text;
using FluentHub.Octokit.Generation.Models;
using FluentHub.Octokit.Generation.Utilities;

namespace FluentHub.Octokit.Generation
{
    internal static class UnionGenerator
    {
        public static string Generate(
            TypeModel type,
            string entityNamespace)
        {
            var className = TypeUtilities.GetClassName(type);

            return $@"namespace {entityNamespace}
{{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    {GenerateUnionDocComments(type)}public class {className}
    {{{GeneratePossibleTypes(type, entityNamespace)}
    }}
}}";
        }

        private static string GeneratePossibleTypes(TypeModel type, string entityNamespace)
        {
            var builder = new StringBuilder();

            if (type.PossibleTypes?.Count > 0)
            {
                var first = true;

                foreach (var field in type.PossibleTypes)
                {
                    if (!first)
                    {
                        builder.AppendLine();
                    }

                    builder.AppendLine();
                    builder.Append(GenerateField(field, entityNamespace));

                    first = false;
                }
            }

            return builder.ToString();
        }

        private static string GenerateField(TypeModel possibleType, string entityNamespace)
        {
            var comments = GenerateDocComments(possibleType);
            var typeName = TypeUtilities.GetClassName(possibleType);
            var name = possibleType.Name;
            return comments + $"        public {name} {name} {{ get; set; }}";
        }

        private static string GenerateUnionDocComments(TypeModel type)
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

        private static string GenerateDocComments(TypeModel possibleType)
        {
            if (!string.IsNullOrWhiteSpace(possibleType.Description))
            {
                var builder = new StringBuilder();
                DocCommentGenerator.GenerateSummary(possibleType.Description, 8, builder);
                return builder.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
