using FluentHub.Octokit.Queries.Repositories;
using FluentHub.App.Extensions;
using FluentHub.App.Helpers;
using FluentHub.App.Services;
using FluentHub.App.Models;
using FluentHub.App.ViewModels.Repositories.Releases;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.Views.Repositories.Releases
{
    public sealed partial class ReleasesPage : Page
    {
        public ReleasesPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<ReleasesViewModel>();
            _navigation = App.Current.Services.GetRequiredService<INavigationService>();
        }

        public ReleasesViewModel ViewModel { get; }
        private readonly INavigationService _navigation;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationArgs;
            ViewModel.Login = param.Login;
            ViewModel.Name = param.Name;

            var command = ViewModel.LoadRepositoryReleasesPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private async void OnLatestReleaseContentWebViewLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.LatestReleaseDescriptionWebView = LatestReleaseContentWebView;
            string missedPath = "https://raw.githubusercontent.com/" + ViewModel.Repository.Owner.Login + "/" + ViewModel.Repository.Name + "/" + ViewModel.Repository.DefaultBranchRef.Name + "/";

            MarkdownApiHandler mdHandler = new();
            var html = await mdHandler.GetHtmlAsync(ViewModel.LatestRelease.DescriptionHTML ?? "<span>No description provided</span>", missedPath, ThemeHelpers.RootTheme.ToString().ToLower());

            LatestReleaseContentWebView.NavigateToString(html);
            await LatestReleaseContentWebView.HandleResize();
        }
    }
}
