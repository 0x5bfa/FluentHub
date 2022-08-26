using System;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Text;

namespace FluentHub.Primer.Octicon.Generation
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
            Console.WriteLine("Where's root dir of FluentHub.Uwp on your local:");
            string root = Console.ReadLine().TrimEnd('\\');

            GenerateEnum(root);
        }

        private static void GenerateEnum(string root)
        {
            // Get all SVGs
            var files = Directory.EnumerateFiles($"{root}\\Assets\\Octicons\\", "*.svg");
            StringBuilder builder = new();

            builder.AppendLine("using System;");
            builder.AppendLine("using System.Collections.Generic;");
            builder.AppendLine("using System.Linq;");
            builder.AppendLine("using System.Text;");
            builder.AppendLine("using System.Threading.Tasks;");

            builder.AppendLine();
            builder.AppendLine("namespace FluentHub.Uwp.Models");
            builder.AppendLine("{");
            builder.AppendLine("    public enum ValidOcticonNames");
            builder.AppendLine("    {");

            int index = 0;

            foreach (var file in files)
            {
                var normalizedFileName = file.ToLower().Replace("-", " ");
                TextInfo info = CultureInfo.CurrentCulture.TextInfo;
                normalizedFileName = info.ToTitleCase(normalizedFileName).Replace(" ", string.Empty);
                Console.WriteLine(normalizedFileName);
                builder.AppendLine($"        {normalizedFileName},");

                index += 1;

                if (files.Count() == index)
                    continue;

                builder.AppendLine();
            }

            builder.AppendLine("    }");
            builder.AppendLine("}");

            Console.Write(builder.ToString());
        }
    }
}
