﻿using System.Text;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Linq;
using Humanizer;

namespace FluentHub.Octicons.Generator
{
	// Convert svg files into a XAML resource dictionary file
	// Assets icons version is 19.5.0 from https://github.com/primer/octicons
	public class Program
	{
		static void Main(string[] args)
		{
			args = args.Append("C:\\Users\\0x5bfa\\source\\repos\\FluentHub\\src\\FluentHub.Octicons.Generator\\").ToArray();

			args = args.Append("C:\\Users\\0x5bfa\\source\\repos\\FluentHub\\src\\FluentHub.App\\Styles\\").ToArray();

			args = args.Append("C:\\Users\\0x5bfa\\source\\repos\\FluentHub\\src\\FluentHub.Core\\Data\\Enums\\").ToArray();

			if (args.Length != 3)
			{
				StringBuilder message = new(1024);

				message.AppendLine();
				message.AppendLine("Copyright (c) 2022-2024 0x5BFA");
				message.AppendLine("Licensed under the MIT License. See the LICENSE.");
				message.AppendLine();
				message.AppendLine("FluentHub Octicons Generator");
				message.AppendLine("--------------------------------------------------");
				message.AppendLine();
				message.AppendLine("An error occurred in parsing command line arguments.");
				message.AppendLine();
				message.AppendLine("Syntax:");
				message.AppendLine("	filename.exe [project folder path]");
				message.AppendLine();

				Console.Write(message.ToString());

				message.Clear();

				return;
			}

			GenerateCSharpEnumEntities(args[0], args[2]);

			GenerateXamlEntities(args[0], args[1]);
		}

		private static void GenerateXamlEntities(string path, string output)
		{
			StringBuilder xamlTemplateBegining = new();
			xamlTemplateBegining.AppendLine(@$"﻿<!--  Copyright (c) 2022-2024 0x5BFA. Licensed under the MIT License. See the LICENSE.  -->");
			xamlTemplateBegining.AppendLine(@$"<!--");
			xamlTemplateBegining.AppendLine(@$"	This code was generated by a tool.");
			xamlTemplateBegining.AppendLine(@$"	Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.");
			xamlTemplateBegining.AppendLine(@$"-->");
			xamlTemplateBegining.AppendLine(@$"<ResourceDictionary");
			xamlTemplateBegining.AppendLine(@$"	xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""");
			xamlTemplateBegining.AppendLine(@$"	xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""");
			xamlTemplateBegining.AppendLine(@$"	xmlns:primer=""using:FluentHub.App.UserControls.PrimerControls"">");
			xamlTemplateBegining.AppendLine(@$"	<ResourceDictionary.MergedDictionaries>");
			xamlTemplateBegining.AppendLine(@$"		<ResourceDictionary>");

			// Delete all existing C# source files
			foreach (var csFile in Directory.EnumerateFiles(path + "Assets", "*.svg"))
			{
				Console.WriteLine($"Converting {csFile.Split('\\').Last()[..^4]} into ColorIcon{NormalizeFileName(csFile)}");

				var convertedStr = ConvertSvgEntity(csFile);

				xamlTemplateBegining.Append(convertedStr);
			}

			xamlTemplateBegining.AppendLine(@$"");
			xamlTemplateBegining.AppendLine(@$"		</ResourceDictionary>");
			xamlTemplateBegining.AppendLine(@$"	</ResourceDictionary.MergedDictionaries>");
			xamlTemplateBegining.AppendLine(@$"</ResourceDictionary>");

			var value = xamlTemplateBegining.ToString();

			File.WriteAllText(Path.Combine(output, "OcticonIcons.xaml"), value);
		}

		private static void GenerateCSharpEnumEntities(string path, string output)
		{
			StringBuilder stringBuilder = new();
			stringBuilder.AppendLine(@$"﻿// Copyright (c) 0x5BFA");
			stringBuilder.AppendLine(@$"// Licensed under the MIT License. See the LICENSE.");
			stringBuilder.AppendLine(@$"");
			stringBuilder.AppendLine(@$"//------------------------------------------------------------------------------");
			stringBuilder.AppendLine(@$"// <auto-generated>");
			stringBuilder.AppendLine(@$"//     This code was generated by a tool.");
			stringBuilder.AppendLine(@$"//");
			stringBuilder.AppendLine(@$"//     Changes to this file may cause incorrect behavior and will be lost if");
			stringBuilder.AppendLine(@$"//     the code is regenerated.");
			stringBuilder.AppendLine(@$"// </auto-generated>");
			stringBuilder.AppendLine(@$"//------------------------------------------------------------------------------");
			stringBuilder.AppendLine(@$"");
			stringBuilder.AppendLine(@$"namespace FluentHub.Core.Data.Enums");
			stringBuilder.AppendLine(@$"{{");
			stringBuilder.AppendLine(@$"	public enum OcticonKind");
			stringBuilder.AppendLine(@$"	{{");

			foreach (var csFile in Directory.EnumerateFiles(path + "Assets", "*.svg"))
			{
				stringBuilder.AppendLine(@$"		Octicon{NormalizeFileName(csFile)},");
			}

			stringBuilder.AppendLine(@$"	}}");
			stringBuilder.AppendLine(@$"}}");

			var value = stringBuilder.ToString();

			File.WriteAllText(Path.Combine(output, "OcticonKind.cs"), value);
		}

		private static string ConvertSvgEntity(string path)
		{
			XmlDocument doc = new();
			doc.Load(path);

			var svgRaw = doc.FirstChild.FirstChild.Attributes.Item(0).InnerText;

			var normalizedFileName = NormalizeFileName(path);
			var iconSize = GetIconSize(normalizedFileName);

			StringBuilder svgXamlTemplate = new();

			svgXamlTemplate.AppendLine(@$"");
			svgXamlTemplate.AppendLine(@$"			<Style x:Key=""Octicon{normalizedFileName}"" TargetType=""primer:Octicon"">");
			svgXamlTemplate.AppendLine(@$"				<Setter Property=""Template"">");
			svgXamlTemplate.AppendLine(@$"					<Setter.Value>");
			svgXamlTemplate.AppendLine(@$"						<ControlTemplate>");
			svgXamlTemplate.AppendLine(@$"							<Viewbox");
			svgXamlTemplate.AppendLine(@$"								Width=""{iconSize}""");
			svgXamlTemplate.AppendLine(@$"								Height=""{iconSize}""");
			svgXamlTemplate.AppendLine(@$"								Stretch=""Uniform"">");
			svgXamlTemplate.AppendLine(@$"								<Canvas Width=""{iconSize}"" Height=""{iconSize}"">");
			svgXamlTemplate.AppendLine(@$"									<Path");
			svgXamlTemplate.AppendLine(@$"										x:Name=""Path1""");
			svgXamlTemplate.AppendLine(@$"										Data=""{svgRaw}""");
			svgXamlTemplate.AppendLine(@$"										Fill=""{{TemplateBinding Foreground}}"" />");
			svgXamlTemplate.AppendLine(@$"								</Canvas>");
			svgXamlTemplate.AppendLine(@$"							</Viewbox>");
			svgXamlTemplate.AppendLine(@$"						</ControlTemplate>");
			svgXamlTemplate.AppendLine(@$"					</Setter.Value>");
			svgXamlTemplate.AppendLine(@$"				</Setter>");
			svgXamlTemplate.AppendLine(@$"			</Style>");

			return svgXamlTemplate.ToString();
		}

		private static string NormalizeFileName(string path)
		{
			string fileName = path.Split('\\').Last();

			string str = fileName[..^4].Replace('-', '_');

			return str.Pascalize();
		}

		private static int GetIconSize(string fileName)
		{
			return Convert.ToInt32(fileName.Substring(fileName.Length - 2));
		}
	}
}
