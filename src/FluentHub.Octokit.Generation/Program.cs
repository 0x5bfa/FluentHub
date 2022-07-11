using Octokit.GraphQL;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace FluentHub.Octokit.Generation
{
    public class Program
    {
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
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var modelPath = Path.Combine(path, "Model");
            if (!Directory.Exists(modelPath))
            {
                Directory.CreateDirectory(modelPath);
            }

            var header = new ProductHeaderValue("FluentHub.Octokit", "1.0");
            var connection = new Connection(header, token);

            Console.WriteLine("Reading from " + connection.Uri);
            var schema = await SchemaReader.ReadSchema(connection);

            foreach (var file in CodeGenerator.Generate(schema, "FluentHub.Octokit", "FluentHub.Octokit.v4.Model"))
            {
                Console.WriteLine("Writing " + file.Path);
                File.WriteAllText(Path.Combine(path, file.Path), file.Content);
            }
        }
    }
}
