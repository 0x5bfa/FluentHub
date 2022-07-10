using FluentHub.Uwp.Services;
using FluentHub.Uwp.Services.Navigation;
using FluentHub.Uwp.ViewModels;
using FluentHub.Uwp.ViewModels.Repositories;
using FluentHub.Uwp.ViewModels.Repositories.Commits;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Repositories.PullRequests
{
    public sealed partial class CommitPage : Page
    {
        public CommitPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<CommitViewModel>();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        public CommitViewModel ViewModel { get; }
        private readonly INavigationService navigationService;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.CommitItem = e.Parameter as Octokit.Models.Commit;
            DataContext = ViewModel;

            var command = ViewModel.LoadCommitPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
