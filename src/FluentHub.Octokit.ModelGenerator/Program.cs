using Octokit.GraphQL;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using FluentHub.Octokit.ModelGenerator.Generators;

namespace FluentHub.Octokit.ModelGenerator
{
    public class Program
    {
        private const string rootNamespace = "FluentHub.Octokit";
        private const string entityNamespace = "FluentHub.Octokit.Models.v4";

        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Error: Invalid arguments.");
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
