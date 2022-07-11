using System;
using System.Text;
using System.Xml;

namespace FluentHub.Octokit.Generation
{
    internal static class DocCommentGenerator
    {
        public static void GenerateSummary(string summary, int indentation, StringBuilder builder)
        {
            if (string.IsNullOrWhiteSpace(summary) is false)
            {
                var indent = new string(' ', indentation);
                builder.Append(indent);
                builder.AppendLine("/// <summary>");
                GenerateCommentBlock(summary, indent, builder);
                builder.Append(indent);
                builder.AppendLine(@"/// </summary>");
            }
        }

        private static void GenerateCommentBlock(string text, string indent, StringBuilder builder)
        {
            text = text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");

            foreach (var line in text.Split('\r', '\n'))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    builder.Append(indent);
                    builder.Append("/// ");
                    builder.AppendLine(line);
                }
            }
        }
    }
}
