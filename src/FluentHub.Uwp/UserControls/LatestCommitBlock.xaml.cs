using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls;
using FluentHub.Uwp.ViewModels.Repositories;
using FluentHub.Uwp.Views.Repositories.Commits;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls
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
            InitializeComponent();

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
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private void OnToggleDisplayCommitMessageButtonClick(object sender, RoutedEventArgs e)
        {
            SubCommitMessagesGrid.Visibility =
                SubCommitMessagesGrid.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void OnViewAllCommitsButtonClick(object sender, RoutedEventArgs e)
        {
            navigationService.Navigate<CommitsPage>(
                new Models.FrameNavigationArgs()
                {
                    Login = ViewModel.ContextViewModel.Repository.Owner.Login,
                    Name = ViewModel.ContextViewModel.Repository.Name,
                    Parameters = new() { ViewModel.ContextViewModel },
                });
        }
    }
}
