using FluentHub.App.Extensions;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.Windows.AppLifecycle;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using static FluentHub.App.Helpers.UWPToWinAppSDKUpgradeHelpers.InteropHelpers;

namespace FluentHub.App
{
    public class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            WinRT.ComWrappersSupport.InitializeComWrappers();
            var proc = System.Diagnostics.Process.GetCurrentProcess();
            var activatedArgs = AppInstance.GetCurrent().GetActivatedEventArgs();

            var activePid = ApplicationData.Current.LocalSettings.Values.Get("INSTANCE_ACTIVE", -1);
            var instance = AppInstance.FindOrRegisterForKey(activePid.ToString());
            if (!instance.IsCurrent)
            {
                RedirectActivationTo(instance, activatedArgs);
                return;
            }

            var currentInstance = AppInstance.FindOrRegisterForKey((-proc.Id).ToString());
            if (currentInstance.IsCurrent)
            {
                currentInstance.Activated += OnActivated;
            }

            ApplicationData.Current.LocalSettings.Values["INSTANCE_ACTIVE"] = -proc.Id;

            Application.Start((p) =>
            {
                var context = new DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread());
                SynchronizationContext.SetSynchronizationContext(context);

                new App();
            });
        }

        private static void OnActivated(object? sender, AppActivationArguments args)
        {
            if (App.Current is App thisApp)
            {
                // WINUI3: verify if needed or OnLaunched is called
                thisApp.OnActivated(args);
            }
        }

        private static IntPtr redirectEventHandle = IntPtr.Zero;

        // WinUI3: https://github.com/microsoft/WindowsAppSDK/issues/1709
        public static void RedirectActivationTo(AppInstance keyInstance, AppActivationArguments args)
        {
            redirectEventHandle = CreateEvent(IntPtr.Zero, true, false, null);

            Task.Run(() =>
            {
                keyInstance.RedirectActivationToAsync(args).AsTask().Wait();
                SetEvent(redirectEventHandle);
            });

            uint CWMO_DEFAULT = 0;
            uint INFINITE = 0xFFFFFFFF;

            _ = CoWaitForMultipleObjects(CWMO_DEFAULT, INFINITE, 1, new IntPtr[] { redirectEventHandle }, out uint handleIndex);
        }
    }
}
