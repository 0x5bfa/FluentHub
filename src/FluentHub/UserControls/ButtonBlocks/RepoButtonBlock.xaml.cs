using FluentHub.ViewModels.UserControls.ButtonBlocks;
using FluentHub.OctokitEx.Models;
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

namespace FluentHub.UserControls.ButtonBlocks
{
    public sealed partial class RepoButtonBlock : UserControl
    {
        #region properties
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(RepositoryBlockItem), typeof(RepoButtonBlockViewModel), typeof(RepoButtonBlock), new PropertyMetadata(null));

        public RepoButtonBlockViewModel ViewModel
        {
            get => (RepoButtonBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                this.DataContext = ViewModel;
                ViewModel?.GetColorBrush();
                UpdateVisibility();
            }
        }
        #endregion

        public RepoButtonBlock()
        {
            this.InitializeComponent();
        }

        public void UpdateVisibility()
        {
            if (ViewModel?.Item?.PrimaryLangName == null)
            {
                LanguageBlock.Visibility = Visibility.Collapsed;
            }

            // does not work property
            if (LicenseBlock != null && string.IsNullOrEmpty(ViewModel?.Item.LicenseName))
            {
                LicenseBlock.Visibility = Visibility.Collapsed;
            }
        }

        private async void RepoBlockButton_Click(object sender, RoutedEventArgs e)
        {
            var repo = await App.Client.Repository.Get(ViewModel.Item.Owner, ViewModel.Item.Name);
            App.MainViewModel.MainFrame.Navigate(typeof(Views.Repositories.OverviewPage), repo.Id.ToString());
        }
    }
}
