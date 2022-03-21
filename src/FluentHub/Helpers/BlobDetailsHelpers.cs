using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Helpers
{
    public class BlobDetailsHelpers
    {
        public static int GetBlobActualLines(ref string text)
        {
            int line = text.Length - text.Replace("\n", "").Length;
            return line;
        }

        public static int GetBlobSloc(ref string text)
        {
            int line = text.Length - text.Replace("\n", "").Length;

            int notsloc = text.Length - text.Replace("\n\n", "").Length;

            // sloc is needed to reduce lines which is not source code(such as comments, black lines)
            return line - notsloc;
        }

        static readonly string[] suffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB" };

        public static string FormatSize(long bytes)
        {
            int counter = 0;
            decimal number = bytes;

            while (Math.Round(number / 1024) >= 1)
            {
                number = number / 1024;
                counter++;
            }

            return string.Format("{0:n2} {1}", number, suffixes[counter]);
        }
    }
}
