using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.Overview;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.Overview
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

        private void OnPullRequestNavViewItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
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
                .OfType<muxc.NavigationViewItem>()
                .FirstOrDefault();

            PullRequestNavView.SelectedItem
                = PullRequestNavView
                .MenuItems
                .OfType<muxc.NavigationViewItem>()
                .FirstOrDefault(x => string.Compare(x.Tag.ToString(), tag?.ToString(), true) == 0)
                ?? defaultItem;
        }
    }
}
