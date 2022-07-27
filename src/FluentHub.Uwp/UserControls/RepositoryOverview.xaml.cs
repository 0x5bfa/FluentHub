using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls
{
    public sealed partial class RepositoryOverview : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(ViewModel),
                typeof(RepositoryOverviewViewModel),
                typeof(RepositoryOverviewViewModel),
                new PropertyMetadata(null));

        public RepositoryOverviewViewModel ViewModel
        {
            get => (RepositoryOverviewViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                if (ViewModel is not null)
                    SelectItemByTag(ViewModel.SelectedTag);
            }
        }
        #endregion

        public RepositoryOverview() => InitializeComponent();

        private void OnRepoPageNavViewItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
            => OnRepoPageNavViewItemSelected(args.InvokedItemContainer.Tag.ToString().ToLower());

        private void OnRepoPageNavViewItemSelected(string tag)
        {
            var service = App.Current.Services.GetRequiredService<INavigationService>();

            string newUrl = $"{App.DefaultGitHubDomain}/{ViewModel.Repository.Owner.Login}/{ViewModel.Repository.Name}";

            switch (tag)
            {
                default:
                case "code":
                    service.Navigate(
                        typeof(Views.Repositories.Codes.CodePage),
                        new Models.FrameNavigationArgs()
                        {
                            Login = ViewModel.Repository.Owner.Login,
                            Name = ViewModel.Repository.Name,
                        });
                    break;
                case "issues":
                    service.Navigate(typeof(Views.Repositories.Issues.IssuesPage), $"{newUrl}/issues");
                    break;
                case "pullrequests":
                    service.Navigate(typeof(Views.Repositories.PullRequests.PullRequestsPage), $"{newUrl}/pulls");
                    break;
                case "discussions":
                    service.Navigate(typeof(Views.Repositories.Discussions.DiscussionsPage), $"{newUrl}/discussions");
                    break;
                case "projects":
                    service.Navigate(typeof(Views.Repositories.Projects.ProjectsPage), $"{newUrl}/projects");
                    break;
                case "insights":
                    service.Navigate(typeof(Views.Repositories.Insights.InsightsPage), $"{newUrl}/pulse");
                    break;
                case "settings":
                    service.Navigate(typeof(Views.Repositories.Settings.SettingsPage), $"{newUrl}/settings");
                    break;
            }
        }

        private void SelectItemByTag(string tag)
        {
            var defaultItem
                = RepoPageNavView
                .MenuItems
                .OfType<muxc.NavigationViewItem>()
                .FirstOrDefault();

            RepoPageNavView.SelectedItem
                = RepoPageNavView
                .MenuItems
                .OfType<muxc.NavigationViewItem>()
                .FirstOrDefault(x => string.Compare(x.Tag.ToString(), tag?.ToString(), true) == 0)
                ?? defaultItem;
        }
    }
}
