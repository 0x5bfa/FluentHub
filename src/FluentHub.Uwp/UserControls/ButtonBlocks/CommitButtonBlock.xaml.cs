using FluentHub.Octokit.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.ButtonBlocks
{
    public sealed partial class CommitButtonBlock : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(CommitButtonBlockViewModel),
                typeof(CommitButtonBlockViewModel),
                typeof(CommitButtonBlock),
                new PropertyMetadata(null));

        public CommitButtonBlockViewModel ViewModel
        {
            get => (CommitButtonBlockViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
        #endregion

        public CommitButtonBlock() => InitializeComponent();

        private void CommitItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CommitItem.PullRequestNumber == 0)
            {
                MainPageViewModel.RepositoryContentFrame.Navigate(typeof(Views.Repositories.Commits.CommitPage), ViewModel.CommitItem);
            }
            else
            {
                MainPageViewModel.PullRequestContentFrame.Navigate(typeof(Views.Repositories.PullRequests.CommitPage), ViewModel.CommitItem);
            }
        }
    }
}
