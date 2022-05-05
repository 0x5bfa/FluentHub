using FluentHub.Octokit.Models;
using FluentHub.Services;
using FluentHub.ViewModels;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Views.Repositories.PullRequests;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.UserControls.ButtonBlocks
{
    public sealed partial class PullButtonBlock : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                  nameof(PullRequest),
                  typeof(PullButtonBlockViewModel),
                  typeof(PullButtonBlock),
                  new PropertyMetadata(null)
                );

        public PullButtonBlockViewModel ViewModel
        {
            get => (PullButtonBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                DataContext = ViewModel;
                ViewModel?.LoadContents();
            }
        }
        #endregion

        public PullButtonBlock()
        {
            InitializeComponent();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        private void IssueBlockButton_Click(object sender, RoutedEventArgs e)
        {
            MainPageViewModel.RepositoryContentFrame.Navigate(typeof(PullRequestPage), ViewModel.PullItem);
        }

        private void OnPullButtonBlockLoaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
