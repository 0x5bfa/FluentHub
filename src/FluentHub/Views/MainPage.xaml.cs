using FluentHub.Services;
using FluentHub.Views.Home;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            CoreApplication.GetCurrentView().TitleBar.LayoutMetricsChanged += OnTitleBarLayoutMetricsChanged;
            navigationService = App.Current.Services.GetService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationService.Configure(TabView, MainFrame, typeof(UserHomePage));
            navigationService.Navigate<UserHomePage>();
        }

        private void OnTitleBarLayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
            => RightPaddingColumn.Width = new GridLength(sender.SystemOverlayRightInset);

        private void DragArea_Loaded(object sender, RoutedEventArgs e) => Window.Current.SetTitleBar(DragArea);

        private void HomeButton_Click(object sender, RoutedEventArgs e) => navigationService.Navigate<UserHomePage>();        
        private void GoBack() => navigationService.GoBack();
        private void GoForward() => navigationService.GoForward();
        private void ToolbarAppSettingsButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void ShareWithBrowserMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            //await Windows.System.Launcher.LaunchUriAsync(new Uri(App.MainViewModel.CurrentPageUrl));
            throw new NotImplementedException();
        }

        private void SettingsMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            navigationService.Navigate<AppSettings.MainSettingsPage>();
        }

        private void SignOutMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            // Temporary treatment (Deletion requested credentials must be deleted)
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(SignIn.IntroPage));
        }
    }
}