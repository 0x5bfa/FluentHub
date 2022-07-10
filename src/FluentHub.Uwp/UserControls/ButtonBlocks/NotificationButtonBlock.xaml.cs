using FluentHub.Octokit.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Uwp.Views.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.ButtonBlocks
{
    public sealed partial class NotificationButtonBlock : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                  nameof(NotificationButtonBlockViewModel),
                  typeof(NotificationButtonBlockViewModel),
                  typeof(NotificationButtonBlock),
                  new PropertyMetadata(null)
                );

        public NotificationButtonBlockViewModel ViewModel
        {
            get => (NotificationButtonBlockViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
        #endregion

        public NotificationButtonBlock()
        {
            this.InitializeComponent();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        private void OnClick(object sender, RoutedEventArgs e)
        {
            switch (ViewModel.Item.ItemState)
            {
                case "IssueClosed":
                case "IssueOpen":
                    navigationService.Navigate<OverviewPage>($"{App.DefaultGitHubDomain}/{ViewModel.Item.Repository.Owner.Login}/{ViewModel.Item.Repository.Name}/issues/{ViewModel.Item.SubjectNumber}");
                    break;
                case "PullMerged":
                case "PullClosed":
                case "PullDraft":
                case "PullOpen":
                    navigationService.Navigate<OverviewPage>($"{App.DefaultGitHubDomain}/{ViewModel.Item.Repository.Owner.Login}/{ViewModel.Item.Repository.Name}/pull/{ViewModel.Item.SubjectNumber}");
                    break;
                case "Discussion":
                    //navigationService.Navigate<OverviewPage>($"{App.DefaultGitHubDomain}/{ViewModel.Item.Repository.Owner.Login}/{ViewModel.Item.Repository.Name}/discussions/{ViewModel.Item.SubjectNumber}");
                    break;
                case "Commit":
                    //navigationService.Navigate<OverviewPage>($"{App.DefaultGitHubDomain}/{ViewModel.Item.Repository.Owner.Login}/{ViewModel.Item.Repository.Name}/commits/{ViewModel.Item.SubjectNumber}");
                    break;
            }
        }
    }
}
