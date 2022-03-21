using FluentHub.ViewModels.Repositories;
using FluentHub.ViewModels.UserControls.Blocks;
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

namespace FluentHub.UserControls.Blocks
{
    public sealed partial class LatestCommitBlock : UserControl
    {
        #region ViewModelProperty
        public static readonly DependencyProperty ViewModelProperty
        = DependencyProperty.Register(
              nameof(CommonRepoViewModel),
              typeof(CommonRepoViewModel),
              typeof(LatestCommitBlock),
              new PropertyMetadata(null)
            );

        public CommonRepoViewModel CommonRepoViewModel
        {
            get => (CommonRepoViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
        #endregion

        public LatestCommitBlock()
        {
            this.InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.CommonRepoViewModel = CommonRepoViewModel;
            await ViewModel.GetCommitDetails();
        }

        private void MoreCommitMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (SubCommitMessagesGrid.Visibility == Visibility.Visible)
            {
                SubCommitMessagesGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                SubCommitMessagesGrid.Visibility = Visibility.Visible;
            }
        }
    }
}
