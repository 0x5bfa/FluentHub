using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels;
using FluentHub.Uwp.Views.Repositories.Codes;
using FluentHub.Uwp.ViewModels.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Repositories
{
    public sealed partial class OverviewPage : Page
    {
        public OverviewPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<OverviewViewModel>();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();

            MainPageViewModel.RepositoryContentFrame.Navigating += OnRepositoryContentFrameNavigating;
        }

        private readonly INavigationService navigationService;
        public OverviewViewModel ViewModel { get; }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var url = e.Parameter as string;
            var uri = new Uri(url);
            var pathSegments = uri.AbsolutePath.Split("/").ToList();
            pathSegments.RemoveAt(0);

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"Repository overview";
            currentItem.Description = currentItem.Header;
            currentItem.Url = url;
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Repositories.png"))
            };

            var command = ViewModel.LoadOverviewPageCommand;
            if (command.CanExecute(url))
                await command.ExecuteAsync(url);

            string tabItem = "";
            string tag;

            if (pathSegments.Count() > 2) tabItem = pathSegments[2];

            switch (tabItem.ToLower())
            {
                default:
                    tag = "code";
                    SelectItemByTag(tag);
                    break;
                case "tree":
                    tag = "code";
                    SelectItemByTag(tag);
                    if (pathSegments.Count() > 3)
                        RepoPageNavViewFrame.Navigate(typeof(CodePage), url);
                    break;
                case "blob":
                    tag = "code";
                    SelectItemByTag(tag);
                    if (pathSegments.Count() > 3)
                        RepoPageNavViewFrame.Navigate(typeof(CodePage), url);
                    break;
                case "issues":
                    tag = "issues";
                    SelectItemByTag(tag);
                    if (pathSegments.Count() > 3)
                        RepoPageNavViewFrame.Navigate(typeof(Issues.IssuePage), url);
                    break;
                case "pull":
                    tag = "pullrequests";
                    SelectItemByTag(tag);
                    RepoPageNavViewFrame.Navigate(typeof(PullRequests.PullRequestPage), url);
                    break;
                case "pulls":
                    tag = "pullrequests";
                    SelectItemByTag(tag);
                    break;
                case "discussions":
                    tag = "discussions";
                    SelectItemByTag(tag);
                    if (pathSegments.Count() > 3)
                        RepoPageNavViewFrame.Navigate(typeof(Discussions.DiscussionPage), url);
                    break;
                case "projects":
                    tag = "projects";
                    SelectItemByTag(tag);
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

            OnRepoPageNavViewItemSelected(tag);
        }

        private void OnRepoPageNavViewItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
            => OnRepoPageNavViewItemSelected(args.InvokedItemContainer.Tag.ToString().ToLower());

        private void OnRepoPageNavViewItemSelected(string tag)
        {
            string newUrl = $"{App.DefaultGitHubDomain}/{ViewModel.Repository.Owner.Login}/{ViewModel.Repository.Name}";

            switch (tag.ToLower())
            {
                default:
                case "code":
                    RepoPageNavViewFrame.Navigate(typeof(CodePage), newUrl);
                    break;
                case "issues":
                    RepoPageNavViewFrame.Navigate(typeof(Issues.IssuesPage), $"{newUrl}/issues");
                    break;
                case "pullrequests":
                    RepoPageNavViewFrame.Navigate(typeof(PullRequests.PullRequestsPage), $"{newUrl}/pulls");
                    break;
                case "discussions":
                    RepoPageNavViewFrame.Navigate(typeof(Discussions.DiscussionsPage), $"{newUrl}/discussions");
                    break;
                case "projects":
                    RepoPageNavViewFrame.Navigate(typeof(Projects.ProjectsPage), $"{newUrl}/projects");
                    break;
                case "insights":
                    RepoPageNavViewFrame.Navigate(typeof(Insights.InsightsPage), $"{newUrl}/pulse");
                    break;
                case "settings":
                    RepoPageNavViewFrame.Navigate(typeof(Settings.SettingsPage), $"{newUrl}/settings");
                    break;
            }
        }

        private void OnRepoOwnerButtonClick(object sender, RoutedEventArgs e)
        {
            var service = App.Current.Services.GetRequiredService<INavigationService>();

            if (ViewModel.Repository.IsInOrganization)
                service.Navigate<Organizations.ProfilePage>(ViewModel.Repository.Owner.Login);
            else
                service.Navigate<Users.ProfilePage>($"{App.DefaultGitHubDomain}/{ViewModel.Repository.Owner.Login}");
        }

        private void OnRepositoryContentFrameNavigating(object sender, NavigatingCancelEventArgs e)
        {
            e.Cancel = true;
            RepoPageNavViewFrame.Navigate(e.SourcePageType, e.Parameter);
        }
    }
}
