using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories.Discussions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.Settings
{
    public sealed partial class GeneralPage : Page
    {
        public GeneralPage()
        {
            this.InitializeComponent();
            var provider = App.Current.Services;
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var url = e.Parameter as string;
            var uri = new Uri(url);
            var pathSegments = uri.AbsolutePath.Split("/").ToList();
            pathSegments.RemoveAt(0);

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Settings";
            currentItem.Description = "Settings";

            currentItem.Icon = new ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Settings.png"))
            };
        }
    }
}