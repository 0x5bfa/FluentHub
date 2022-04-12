using FluentHub.Services;
using FluentHub.ViewModels.UserControls.Blocks;
using FluentHub.ViewModels.Repositories;
using FluentHub.Views.Repositories.Commits;
using FluentHub.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.UserControls.Blocks
{
    public sealed partial class LatestCommitBlock : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ContextViewModelProperty =
            DependencyProperty.Register(
              nameof(ContextViewModel),
              typeof(RepoContextViewModel),
              typeof(LatestCommitBlock),
              new PropertyMetadata(null)
            );

        public RepoContextViewModel ContextViewModel
        {
            get => (RepoContextViewModel)GetValue(ContextViewModelProperty);
            set => SetValue(ContextViewModelProperty, value);
        }
        #endregion

        public LatestCommitBlock()
        {
            this.InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<LatestCommitBlockViewModel>();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        public LatestCommitBlockViewModel ViewModel { get; }
        private readonly INavigationService navigationService;

        private void OnLatestCommitBlockLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.ContextViewModel = ContextViewModel;

            var command = ViewModel.LoadLatestCommitBlockCommand;
            if (command.CanExecute(DataContext))
                command.Execute(DataContext);
        }

        private void MoreCommitMessageButton_Click(object sender, RoutedEventArgs e)
        {
            SubCommitMessagesGrid.Visibility =
                SubCommitMessagesGrid.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPageViewModel.RepositoryContentFrame.Navigate(typeof(CommitsPage), ViewModel.ContextViewModel);
        }
    }
}
