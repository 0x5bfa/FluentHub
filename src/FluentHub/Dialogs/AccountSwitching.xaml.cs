using FluentHub.Services;
using FluentHub.Services.Navigation;
using FluentHub.ViewModels.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Dialogs
{
    public sealed partial class AccountSwitching : ContentDialog
    {
        public AccountSwitching()
        {
            this.InitializeComponent();
            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<AccountSwitchingDialogViewModel>();
        }

        public AccountSwitchingDialogViewModel ViewModel { get; }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e) => Hide();

        private async void OnAddAccountButtonClick(object sender, RoutedEventArgs e)
        {
        }

        private void OnAccountSwitchingDialogLoaded(object sender, RoutedEventArgs e)
        {
            var command = ViewModel.LoadSignedInLoginsCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
