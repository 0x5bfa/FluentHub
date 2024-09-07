// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Octokit.ModelGenerator.Generators;
using Octokit.GraphQL;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.ModelGenerator
{
	public class Program
	{
		private readonly static string rootNamespace = "FluentHub.Octokit";

		private readonly static string entityNamespace = "FluentHub.Octokit.Models.v4";

		static void Main(string[] args)
		{
			if (args.Length != 2)
			{
				StringBuilder message = new(1024);

				message.AppendLine();
				message.AppendLine("Copyright (c) 2022-2024 0x5BFA");
				message.AppendLine("Licensed under the MIT License. See the LICENSE.");
				message.AppendLine();
				message.AppendLine("FluentHub Octokit Model Generator");
				message.AppendLine("--------------------------------------------------");
				message.AppendLine();
				message.AppendLine("An error occurred in parsing command line arguments.");
				message.AppendLine();
				message.AppendLine("Syntax:");
				message.AppendLine("	filename.exe [api token] [path to copy]");
				message.AppendLine();

				Console.Write(message.ToString());

				message.Clear();

				return;
			}

			GenerateEntities(args[0], args[1]).Wait();
		}

		private static async Task GenerateEntities(string token, string path)
		{
			if (Directory.Exists(path) is false)
				Directory.CreateDirectory(path);

			// Delete all existing C# source files
			foreach (var csFile in Directory.EnumerateFiles(path, "*.cs"))
				File.Delete(csFile);

			var header = new ProductHeaderValue("FluentHub.Octokit", "1.0");
			var connection = new Connection(header, token);

			Console.WriteLine("Reading from " + connection.Uri);
			var schema = await SchemaReader.ReadSchema(connection);

			foreach (var file in CodeGenerator.Generate(schema, rootNamespace, entityNamespace))
			{
				Console.WriteLine("Writing " + file.Path);
				File.WriteAllText(Path.Combine(path, file.Path), file.Content);
			}
		}
	}
}
