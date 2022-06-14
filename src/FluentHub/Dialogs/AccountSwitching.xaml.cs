using FluentHub.Services;
using FluentHub.Services.Navigation;
using FluentHub.ViewModels.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Dialogs
{
    public sealed partial class AccountSwitching : ContentDialog
    {
        public AccountSwitching()
        {
            InitializeComponent();
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e) => Hide();
    }
}
