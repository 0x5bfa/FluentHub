using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Extensions;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.ViewModels.Repositories.Releases;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Imaging;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Repositories.Releases
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
            var html = await mdHandler.GetHtmlAsync(ViewModel.LatestRelease.DescriptionHTML ?? "<span>No description</span>", missedPath, ThemeHelper.ActualTheme.ToString().ToLower());

            LatestReleaseContentWebView.NavigateToString(html);
            await LatestReleaseContentWebView.HandleResize();
        }
    }
}
