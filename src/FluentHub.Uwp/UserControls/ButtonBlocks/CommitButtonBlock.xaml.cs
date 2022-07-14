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
            string url = $"https://github.com/{ViewModel.CommitItem.Repository.Owner.Login}/{ViewModel.CommitItem.Repository.Name}/";
            Type frameType;

            if (ViewModel.PullRequest == null)
            {
                url += $"commit/{ViewModel.CommitItem.Oid}";
                frameType = typeof(Views.Repositories.Commits.CommitPage);
                MainPageViewModel.RepositoryContentFrame.Navigate(frameType, url);
            }
            else
            {
                url += $"pull/{ViewModel.PullRequest.Number}/commits/{ViewModel.CommitItem.Oid}";
                frameType = typeof(Views.Repositories.PullRequests.CommitPage);
                MainPageViewModel.PullRequestContentFrame.Navigate(frameType, url);
            }
        }
    }
}
