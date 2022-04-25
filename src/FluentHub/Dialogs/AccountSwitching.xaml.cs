using FluentHub.Services;
using FluentHub.Services.Navigation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace FluentHub.Dialogs
{
    public sealed partial class AccountSwitching : ContentDialog
    {
        private INavigationService NavigationService { get; }
        public AccountSwitching()
        {
            this.InitializeComponent();
            var provider = App.Current.Services;
            NavigationService = provider.GetRequiredService<INavigationService>();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            NavigationService.Navigate<Views.SignIn.IntroPage>();
        }
    }
}