using FluentHub.Octokit.Models;
using FluentHub.Uwp.ViewModels;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Uwp.Views.Repositories.Projects;
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

namespace FluentHub.Uwp.UserControls.ButtonBlocks
{
    public sealed partial class ProjectButtonBlock : UserControl
    {
        #region dprops
        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                  nameof(Project),
                  typeof(ProjectButtonBlockViewModel),
                  typeof(ProjectButtonBlock),
                  new PropertyMetadata(null)
                );

        public ProjectButtonBlockViewModel ViewModel
        {
            get => (ProjectButtonBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                DataContext = ViewModel;
            }
        }
        #endregion

        public ProjectButtonBlock() => InitializeComponent();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPageViewModel.RepositoryContentFrame.Navigate(typeof(ProjectPage), ViewModel.Item.Url);
        }
    }
}
