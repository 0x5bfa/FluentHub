using FluentHub.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Users
{
    public sealed partial class FollowingPage : Page
    {
        public FollowingPage()
        {
            InitializeComponent();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string login = e.Parameter as string;

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"Following";
            currentItem.Description = $"{login}'s followers";
            currentItem.Url = $"https://github.com/{login}?tab=following";
            currentItem.Icon = new Microsoft.UI.Xaml.Controls.FontIconSource
            {
                Glyph = "\uE737"
            };

            await ViewModel.GetFollowingList(login);

            base.OnNavigatedTo(e);
        }
    }
}