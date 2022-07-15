using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Users
{
    public sealed partial class StarredReposPage : Page
    {
        public StarredReposPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<StarredReposViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public StarredReposViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var url = e.Parameter as string;
            var uri = new Uri(url);
            var pathSegments = uri.AbsolutePath.Split("/").ToList();
            pathSegments.RemoveAt(0);

            string login;

            if (url == "fluenthub://stars")
            {
                login = App.Settings.SignedInUserName;
                ViewModel.DisplayTitle = true;
            }
            else
            {
                login = pathSegments[0];
            }

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"Stars";
            currentItem.Description = $"{login}'s stars";
            currentItem.DisplayUrl = $"{login} / Stars";
            currentItem.Url = url;
            currentItem.Icon = new Microsoft.UI.Xaml.Controls.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Starred.png"))
            };

            var command = ViewModel.RefreshRepositoriesCommand;
            if (command.CanExecute(login))
                command.Execute(login);
        }
    }
}
