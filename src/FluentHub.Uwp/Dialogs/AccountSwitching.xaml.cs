using FluentHub.Uwp.Services;
using FluentHub.Uwp.Services.Navigation;
using FluentHub.Uwp.ViewModels.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.Dialogs
{
    public sealed partial class AccountSwitching : ContentDialog
    {
        public AccountSwitching()
            => InitializeComponent();

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
            => Hide();
    }
}
