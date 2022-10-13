using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.UserControls.Overview;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.Overview
{
    public sealed partial class PullRequestOverview : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(ViewModel),
                typeof(PullRequestOverviewViewModel),
                typeof(PullRequestOverviewViewModel),
                new PropertyMetadata(null));

        public PullRequestOverviewViewModel ViewModel
        {
            get => (PullRequestOverviewViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                if (ViewModel is not null)
                    SelectItemByTag(ViewModel.SelectedTag);
            }
        }
        #endregion

        public PullRequestOverview()
        {
            InitializeComponent();
        }

        private void OnPullRequestNavViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var service = App.Current.Services.GetRequiredService<INavigationService>();

            switch (args.InvokedItemContainer.Tag.ToString().ToLower())
            {
                default:
                case "conversation":
                    service.Navigate(
                        typeof(Views.Repositories.PullRequests.ConversationPage),
                        new Models.FrameNavigationArgs()
                        {
                            Login = ViewModel.PullRequest?.Repository?.Owner?.Login,
                            Name = ViewModel.PullRequest?.Repository?.Name,
                            Number = ViewModel.PullRequest.Number,
                        });
                    break;
                case "commits":
                    service.Navigate(
                        typeof(Views.Repositories.PullRequests.CommitsPage),
                        new Models.FrameNavigationArgs()
                        {
                            Login = ViewModel.PullRequest?.Repository?.Owner?.Login,
                            Name = ViewModel.PullRequest?.Repository?.Name,
                            Number = ViewModel.PullRequest.Number,
                        });
                    break;
                case "checks":
                    break;
                case "filechanges":
                    service.Navigate(
                        typeof(Views.Repositories.PullRequests.FileChangesPage),
                        new Models.FrameNavigationArgs()
                        {
                            Login = ViewModel.PullRequest?.Repository?.Owner?.Login,
                            Name = ViewModel.PullRequest?.Repository?.Name,
                            Number = ViewModel.PullRequest.Number,
                        });
                    break;
            }
        }

        private void SelectItemByTag(string tag)
        {
            var defaultItem
                = PullRequestNavView
                .MenuItems
                .OfType<NavigationViewItem>()
                .FirstOrDefault();

            PullRequestNavView.SelectedItem
                = PullRequestNavView
                .MenuItems
                .OfType<NavigationViewItem>()
                .FirstOrDefault(x => string.Compare(x.Tag.ToString(), tag?.ToString(), true) == 0)
                ?? defaultItem;
        }
    }
}
