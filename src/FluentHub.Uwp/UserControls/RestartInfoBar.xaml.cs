using Serilog;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Uwp.UserControls
{
    public sealed partial class RestartInfoBar : UserControl
    {
        public RestartInfoBar() => InitializeComponent();

        private async void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            await CoreApplication.RequestRestartAsync("Application was restarted to apply settings.");
            Log.Debug("Application was restared by user.");
        }
    }
}
