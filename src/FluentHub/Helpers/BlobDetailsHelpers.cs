using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Helpers
{
    public class BlobDetailsHelpers
    {
        public static (int, int, string) GetBlobDetails(ref string text, long bytes)
        {
            int line = text.Length - text.Replace("\n", "").Length;

            int maxSloc = 0;

            for (int i = 0; i < text.Length; i++)
            {
                int index = text.IndexOf("\n", i);

                if (maxSloc < (index - i))
                {
                    maxSloc = index - i;
                }

                i = index + 1;
            }

            string formattedSize =  FormatSize(bytes);

            return (line, maxSloc, formattedSize);
        }

        static readonly string[] suffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB" };

        private static string FormatSize(long bytes)
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
