using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FluentHub.Octokit.Logger
{
    internal class LogWriter
    {
        private string logFile;

        public LogWriter()
        {
            var today = DateTime.Today;
            logFile = $"Log{today.Year}{today.Month}{today.Day}";
        }

        public async Task WriteLineToLogAsync(string text)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.GetFileAsync("sample.txt");
        }
    }
}
