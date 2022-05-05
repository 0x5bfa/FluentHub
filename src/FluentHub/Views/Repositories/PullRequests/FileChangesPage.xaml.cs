using FluentHub.Services;
using FluentHub.ViewModels.Repositories.PullRequests;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Repositories.PullRequests
{
    public sealed partial class FileChangesPage : Page
    {
        public FileChangesPage()
        {
            this.InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<FileChangesViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public FileChangesViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Octokit.Models.PullRequest param = e.Parameter as Octokit.Models.PullRequest;

            DataContext = e.Parameter;

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"{param.Title} · Pull Request #{param.Number} · {param.OwnerLogin}/{param.Name}";
            currentItem.Description = currentItem.Header;
            currentItem.Url = $"https://github.com/{param.OwnerLogin}/{param.Name}/pull/{param.Number}";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/PullRequests.png"))
            };

            var command = ViewModel.RefreshPullRequestPageCommand;
            if (command.CanExecute(param))
                command.Execute(param);
        }
    }
}
