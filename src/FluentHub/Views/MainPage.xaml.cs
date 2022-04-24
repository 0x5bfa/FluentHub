using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Services.Navigation;
using FluentHub.ViewModels;
using FluentHub.Views.Home;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using Windows.ApplicationModel.Core;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views
{
    public sealed partial class MainPage : Page
    {
        #region constructor
        public MainPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<MainPageViewModel>();
            NavigationService = provider.GetRequiredService<INavigationService>();
            Logger = provider.GetService<ILogger>();
        }
        #endregion

        #region properties
        public MainPageViewModel ViewModel { get; }
        private INavigationService NavigationService { get; }
        public ILogger Logger { get; }
        #endregion

        #region methods
        private void SubscribeEvents()
        {
            var titleBar = CoreApplication.GetCurrentView().TitleBar;
            titleBar.LayoutMetricsChanged += OnTitleBarLayoutMetricsChanged;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnAppBackRequested;
            Window.Current.CoreWindow.PointerPressed += OnWindowPointerPressed;
        }

        private void UnsubscribeEvents()
        {
            var titleBar = CoreApplication.GetCurrentView().TitleBar;
            titleBar.LayoutMetricsChanged -= OnTitleBarLayoutMetricsChanged;
            SystemNavigationManager.GetForCurrentView().BackRequested -= OnAppBackRequested;
            Window.Current.CoreWindow.PointerPressed -= OnWindowPointerPressed;
        }

        private Microsoft.UI.Xaml.Controls.InfoBarSeverity UserNotificationToInfoBarSeverity(UserNotificationType type)
        {
            return type switch
            {
                UserNotificationType.Info => Microsoft.UI.Xaml.Controls.InfoBarSeverity.Informational,
                UserNotificationType.Success => Microsoft.UI.Xaml.Controls.InfoBarSeverity.Success,
                UserNotificationType.Warning => Microsoft.UI.Xaml.Controls.InfoBarSeverity.Warning,
                UserNotificationType.Error => Microsoft.UI.Xaml.Controls.InfoBarSeverity.Error,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
            };
        }
        #endregion

        #region event handlers
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            SubscribeEvents();
            TabView.NewTabPage = typeof(UserHomePage);
            NavigationService.Configure(TabView);
            NavigationService.Navigate<UserHomePage>();

            // Configure Jumplist
            await JumpListHelper.ConfigureDefaultJumpListAsync();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            UnsubscribeEvents();
            NavigationService.Disconnect();
        }

        private void OnTitleBarLayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
           => RightPaddingColumn.Width = new GridLength(sender.SystemOverlayRightInset);

        private void OnAppBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (NavigationService.CanGoBack())
            {
                NavigationService.GoBack();
                e.Handled = true;
            }

            Logger?.Debug("SystemNavigationManager.GetForCurrentView().BackRequested fired, [handled: {0}]", e.Handled);
        }

        private void OnWindowPointerPressed(CoreWindow sender, PointerEventArgs e)
        {
            // Mouse back button pressed
            if (e.CurrentPoint.Properties.IsXButton1Pressed)
            {
                bool canGoBack = NavigationService.CanGoBack();
                if (canGoBack)
                {
                    NavigationService.GoBack();
                    e.Handled = true;
                }
                Logger?.Debug("CoreWindow.PointerPressed [button: {0}, canGoBack: {1}]",
                    e.CurrentPoint.Properties.PointerUpdateKind,
                    canGoBack);
            }
            // Mouse forward button pressed
            else if (e.CurrentPoint.Properties.IsXButton2Pressed)
            {
                bool canGoForward = NavigationService.CanGoForward();
                if (canGoForward)
                {
                    NavigationService.GoForward();
                    e.Handled = true;
                }
                Logger?.Debug("CoreWindow.PointerPressed [button: {0}, CanGoForward: {1}]",
                    e.CurrentPoint.Properties.PointerUpdateKind,
                    canGoForward);
            }
        }

        private void OnDragAreaLoaded(object sender, RoutedEventArgs e) => Window.Current.SetTitleBar(DragArea);

        private async void ShareWithBrowserMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var currentItem = NavigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            var result = await Launcher.LaunchUriAsync(new Uri(currentItem.Url));
            Logger?.Debug("Launcher.LaunchUriAsync fired, [result: {0}]", result);
        }

        private void SettingsMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
            => NavigationService.Navigate<AppSettings.MainSettingsPage>();

        private void SignOutMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            // Temporary treatment (Deletion requested credentials must be deleted)
            Frame rootFrame = (Frame)Window.Current.Content;
            rootFrame.Navigate(typeof(SignIn.IntroPage));
        }

        private void OnTabViewSelectionChanged(object sender, TabViewSelectionChangedEventArgs e)
            => RootFrameBorder.Child = e.NewSelectedItem?.Frame;
        #endregion

        private void OnUrlTextBoxKeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
            }
        }
    }
}
