using FluentHub.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Users
{
    public sealed partial class IssuesPage : Page
    {
        public IssuesPage()
        {
            InitializeComponent();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string login = e.Parameter as string;

            //Helpers.NavigationHelpers.AddPageInfoToTabItem($"Issues", "Viewer's issues", $"https://github.com/issues", "\uE737");
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Issues".GetLocalized();
            currentItem.Description = "Viewer's issues";
            currentItem.Url = $"https://github.com/issues";
            currentItem.Icon = new Microsoft.UI.Xaml.Controls.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/JumpListIcons/Issues.png"))
            };

            await ViewModel.GetRepoIssues(login);
        }
    }
}