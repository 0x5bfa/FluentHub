using FluentHub.Services;
using FluentHub.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Users
{
    public sealed partial class OverviewPage : Page
    {
        public OverviewPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<OverviewViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public OverviewViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // e.g.) https://github.com/onein528
            string url = e.Parameter as string;
            var uri = new Uri(url);
            string login = ViewModel.LoginName = uri.Segments[1];

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"{login}";
            currentItem.Description = $"{login}'s overview";
            currentItem.Url = url;
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Profile.png"))
            };

            var command = ViewModel.RefreshRepositoryCommand;
            if (command.CanExecute(login))
                command.Execute(login);
        }
    }
}
