﻿using FluentHub.Dialogs;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FluentHub.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            this.InitializeComponent();
        }

        private async void SetupButton_Click(object sender, RoutedEventArgs e)
        {
            SetupDialog dialog = new SetupDialog();

            _ = await dialog.ShowAsync();
        }
    }
}
