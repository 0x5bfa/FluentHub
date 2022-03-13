using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.System;

namespace FluentHub.ViewModels.AppSettings
{
    public class AboutViewModel
    {
        public string Version
        {
            get
            {
                string architecture = Package.Current.Id.Architecture.ToString();

#if DEBUG
                string buildConfiguration = "DEBUG";
#else
                string buildConfiguration = "RELEASE";
#endif

                return $"{App.AppVersion} | {architecture} | {buildConfiguration}";
            }
        }

        public AboutViewModel()
        {
            CopyVersionCommand = new RelayCommand(ExecuteCopyVersionCommand);
            OpenLogsCommand = new AsyncRelayCommand(ExecuteOpenLogsCommandAsync);
        }

        internal IRelayCommand CopyVersionCommand { get; }

        private void ExecuteCopyVersionCommand()
        {
            try
            {
                var data = new DataPackage
                {
                    RequestedOperation = DataPackageOperation.Copy
                };

                data.SetText(Version);

                Clipboard.SetContentWithOptions(data, new ClipboardContentOptions() { IsAllowedInHistory = true, IsRoamable = true });
                Clipboard.Flush();
            }
            catch (Exception ex)
            {
                Log.Warning($"Failed to copy data from copy version command, version: ${Version}", ex);
            }
        }

        internal IAsyncRelayCommand OpenLogsCommand { get; }

        private async Task ExecuteOpenLogsCommandAsync()
        {
            string logsFolder = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Logs");
            await Launcher.LaunchFolderPathAsync(logsFolder);
        }
    }
}
