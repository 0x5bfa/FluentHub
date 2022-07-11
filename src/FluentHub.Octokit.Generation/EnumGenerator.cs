using System;
using System.Globalization;
using System.Text;
using Octokit.GraphQL.Core.Utilities;
using FluentHub.Octokit.Generation.Models;
using FluentHub.Octokit.Generation.Utilities;

namespace FluentHub.Octokit.Generation
{
    public static class EnumGenerator
    {
        public static string Generate(TypeModel type, string entityNamespace)
        {
            var enumName = TypeUtilities.GetClassName(type);

            return $@"using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace {entityNamespace}
{{
    {GenerateDocComments(type)}[JsonConverter(typeof(StringEnumConverter))]
    public enum {enumName}
    {{{GenerateEnumValues(type)}    }}
}}";
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
                    ? $@"        {AttributeGenerator.GenerateObsoleteAttribute(value.DeprecationReason)}{Environment.NewLine}"
                    : string.Empty;
            
            return $@"{obsoleteAttribute}        [EnumMember(Value = ""{value.Name}"")]
        {value.Name.SnakeCaseToPascalCase()},";
        }
    }
}
