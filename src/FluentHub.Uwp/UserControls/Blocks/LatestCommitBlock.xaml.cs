using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.Blocks;
using FluentHub.Uwp.ViewModels.Repositories;
using FluentHub.Uwp.Views.Repositories.Commits;
using FluentHub.Uwp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Uwp.UserControls.Blocks
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
