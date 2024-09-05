// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using System.Text;

namespace FluentHub.Octokit.ModelGenerator.Generators
{
	internal static class DocCommentGenerator
	{
		public static void GenerateSummary(string summary, int indentation, StringBuilder builder)
		{
			if (!string.IsNullOrWhiteSpace(summary))
			{
				var indent = new string('	', indentation / 4);
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
