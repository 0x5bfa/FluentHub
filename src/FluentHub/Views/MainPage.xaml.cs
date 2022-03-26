using FluentHub.Helpers;
using FluentHub.Services;
using FluentHub.Views.Home;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.ApplicationModel.Core;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            CoreApplication.GetCurrentView().TitleBar.LayoutMetricsChanged += TitleBar_LayoutMetricsChanged;
            navigationService = App.Current.Services.GetService<INavigationService>();

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

        private void TitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            CoreApplication.GetCurrentView().TitleBar.LayoutMetricsChanged += OnTitleBarLayoutMetricsChanged;
        }

        private void OnTitleBarLayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
            => RightPaddingColumn.Width = new GridLength(sender.SystemOverlayRightInset);

        private void DragArea_Loaded(object sender, RoutedEventArgs e) => Window.Current.SetTitleBar(DragArea);

        private void HomeButton_Click(object sender, RoutedEventArgs e) => navigationService.Navigate<UserHomePage>();
        private void GoBack() => navigationService.GoBack();
        private void GoForward() => navigationService.GoForward();
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

        private void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            var handled = true;
            var ka = args.KeyboardAccelerator;
            switch (ka.Key)
            {
                case VirtualKey.T when ka.Modifiers == VirtualKeyModifiers.Control:
                    navigationService.OpenTab<UserHomePage>();
                    break;

                case VirtualKey.W when ka.Modifiers == VirtualKeyModifiers.Control:
                    navigationService.CloseTab(navigationService.TabView.SelectedItem.Guid);
                    break;

                case VirtualKey.Tab when ka.Modifiers == VirtualKeyModifiers.Control:
                    if (navigationService.TabView.SelectedIndex == navigationService.TabView.Items.Count - 1)
                    {
                        navigationService.TabView.SelectedIndex = 0;
                    }
                    else
                    {
                        navigationService.TabView.SelectedIndex++;
                    }
                    break;

                case VirtualKey.Tab when ka.Modifiers == (VirtualKeyModifiers.Shift | VirtualKeyModifiers.Control):
                    if (navigationService.TabView.SelectedIndex == 0)
                    {
                        navigationService.TabView.SelectedIndex = navigationService.TabView.Items.Count - 1;
                    }
                    else
                    {
                        navigationService.TabView.SelectedIndex--;
                    }
                    break;

                case VirtualKey.Left when ka.Modifiers == VirtualKeyModifiers.Menu && navigationService.CanGoBack():
                    navigationService.GoBack();
                    break;

                case VirtualKey.Right when ka.Modifiers == VirtualKeyModifiers.Menu && navigationService.CanGoForward():
                    navigationService.GoForward();
                    break;

                default:
                    handled = false;
                    break;
            }
            args.Handled = handled;
        }
    }
}