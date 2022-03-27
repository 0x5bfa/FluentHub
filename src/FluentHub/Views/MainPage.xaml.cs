using FluentHub.Helpers;
using FluentHub.Services;
using FluentHub.ViewModels;
using FluentHub.Views.Home;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.ApplicationModel.Core;
using Windows.System;
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
            CoreApplication.GetCurrentView().TitleBar.LayoutMetricsChanged += (CoreApplicationViewTitleBar sender, object args) => RightPaddingColumn.Width = new GridLength(sender.SystemOverlayRightInset);
            DragArea.Loaded += (_, _) => Window.Current.SetTitleBar(DragArea);

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<MainPageViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();

            // Handle BackRequested event
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) =>
            {
                if (navigationService.CanGoBack())
                {
                    navigationService.GoBack();
                    e.Handled = true;
                }
            };

            // Handle mouse button events (back and forward)
            Window.Current.CoreWindow.PointerPressed += (s, e) =>
            {
                if (e.CurrentPoint.Properties.IsXButton1Pressed && navigationService.CanGoBack()) // Mouse back button pressed
                {
                    navigationService.GoBack();
                    e.Handled = true;
                }
                else if (e.CurrentPoint.Properties.IsXButton2Pressed && navigationService.CanGoForward()) // Mouse forward button pressed
                {
                    navigationService.GoForward();
                    e.Handled = true;
                }
            };
        }

        public MainPageViewModel ViewModel { get; }
        private readonly INavigationService navigationService;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationService.Configure(TabView, MainFrame, typeof(UserHomePage));
            navigationService.Navigate<UserHomePage>();

            // Configure Jumplist
            await JumpListHelper.ConfigureDefaultJumpListAsync();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            navigationService.Disconnect();
        }

        private async void ShareWithBrowserMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            var result = await Launcher.LaunchUriAsync(new Uri(currentItem.Url));
            System.Diagnostics.Debug.WriteLine("LaunchUriAsync({0}) - result:{1}", currentItem.Url, result);
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