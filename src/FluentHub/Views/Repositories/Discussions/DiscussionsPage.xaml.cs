using FluentHub.Services;
using FluentHub.ViewModels.Repositories.Discussions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Repositories.Discussions
{
    public sealed partial class DiscussionsPage : Page
    {
        public DiscussionsPage()
        {
            this.InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<DiscussionsViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public DiscussionsViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var nameWithOwner = e.Parameter as string;
            var nameAndOwner = nameWithOwner.Split("/");

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Discussions";
            currentItem.Description = "Discussions";
            currentItem.Url = $"https://github.com/{nameAndOwner[0]}/{nameAndOwner[1]}/pulls";
            currentItem.Icon = new Microsoft.UI.Xaml.Controls.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/PullRequests.png"))
            };

            var command = ViewModel.LoadDiscussionsPageCommand;
            if (command.CanExecute(nameWithOwner))
                command.Execute(nameWithOwner);
        }
    }
}
