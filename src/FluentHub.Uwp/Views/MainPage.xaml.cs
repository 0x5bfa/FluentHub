using FluentHub.Uwp.Dialogs;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.Services.Navigation;
using FluentHub.Uwp.ViewModels;
using FluentHub.Uwp.Utils;
using Microsoft.Extensions.DependencyInjection;
using Windows.ApplicationModel.Core;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<MainPageViewModel>();
            NavigationService = provider.GetRequiredService<INavigationService>();
            Logger = provider.GetService<ILogger>();
        }

        #region Fields and Properties
        public MainPageViewModel ViewModel { get; }
        private INavigationService NavigationService { get; }
        public ILogger Logger { get; }
        #endregion

        #region Methods
        private void SubscribeEvents()
        {
            var titleBar = CoreApplication.GetCurrentView().TitleBar;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnAppBackRequested;
            Window.Current.CoreWindow.PointerPressed += OnWindowPointerPressed;
        }

        private void UnsubscribeEvents()
        {
            var titleBar = CoreApplication.GetCurrentView().TitleBar;
            SystemNavigationManager.GetForCurrentView().BackRequested -= OnAppBackRequested;
            Window.Current.CoreWindow.PointerPressed -= OnWindowPointerPressed;
        }

        private void OnUrlTextBoxFocus(object sender, RoutedEventArgs e)
        {
            TextBox DisplayUrlTextBox = sender as TextBox;
            if (DisplayUrlTextBox != null && TabView.SelectedItem.NavigationHistory.CurrentItem.Url != null)
            {
                DisplayUrlTextBox.Text = TabView.SelectedItem.NavigationHistory.CurrentItem.Url;
                DisplayUrlTextBox.SelectAll();
            }
        }

        private void OnUrlTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox DisplayUrlTextBox = sender as TextBox;
            if (DisplayUrlTextBox != null)
            {
                DisplayUrlTextBox.Text = TabView.SelectedItem.NavigationHistory?.CurrentItem?.DisplayUrl;
            }
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

        #region Event handlers
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            SubscribeEvents();
            TabView.NewTabPage = typeof(Home.UserHomePage);
            NavigationService.Configure(TabView);
            NavigationService.Navigate<Home.UserHomePage>();

            var command = ViewModel.LoadSignedInUserCommand;
            if (command.CanExecute(null))
                command.Execute(null);

            // Configure Jumplist
            await JumpListHelper.ConfigureDefaultJumpListAsync();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            UnsubscribeEvents();
            NavigationService.Disconnect();
        }

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

        private async void OnContinueWithYourBrowserMenuFlyoutItemClick(object sender, RoutedEventArgs e)
        {
            var currentItem = NavigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            var result = await Launcher.LaunchUriAsync(new Uri(currentItem.Url));
            Logger?.Debug("Launcher.LaunchUriAsync fired, [result: {0}]", result);
        }

        private void OnYourProfileMenuFlyoutItemClick(object sender, RoutedEventArgs e)
            => NavigationService.Navigate<Users.ProfilePage>($"{App.DefaultGitHubDomain}/{App.Settings.SignedInUserName}");

        private void OnYourRepositoriesMenuFlyoutItemClick(object sender, RoutedEventArgs e)
            => NavigationService.Navigate<Users.RepositoriesPage>("fluenthub://repositories");

        private void OnYourDiscussionsMenuFlyoutItemClick(object sender, RoutedEventArgs e)
            => NavigationService.Navigate<Users.DiscussionsPage>("fluenthub://discussions");

        private void OnYourIssuesMenuFlyoutItemClick(object sender, RoutedEventArgs e)
            => NavigationService.Navigate<Users.IssuesPage>("https://github.com/issues");

        private void OnYourPullRequestsMenuFlyoutItemClick(object sender, RoutedEventArgs e)
            => NavigationService.Navigate<Users.PullRequestsPage>("https://github.com/pulls");

        private void OnYourOrganizationsMenuFlyoutItemClick(object sender, RoutedEventArgs e)
            => NavigationService.Navigate<Users.OrganizationsPage>("fluenthub://organizations");

        private void OnYourStarsMenuFlyoutItemClick(object sender, RoutedEventArgs e)
            => NavigationService.Navigate<Users.StarredReposPage>("fluenthub://stars");

        private void OnAppSettingsMenuFlyoutItemClick(object sender, RoutedEventArgs e)
            => NavigationService.Navigate<AppSettings.MainSettingsPage>("fluenthub://settings");

        private void OnAccountSettingsMenuFlyoutItemClick(object sender, RoutedEventArgs e)
            => NavigationService.Navigate<AppSettings.MainSettingsPage>("fluenthub://settings/account");

        private void OnSignOutMenuFlyoutItemClick(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = (Frame)Window.Current.Content;
            rootFrame.Navigate(typeof(SignIn.IntroPage));
        }

        private async void SwitchAccountFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            AccountSwitching accountSwitchingDialog = new();
            await accountSwitchingDialog.ShowAsync();
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

        private void OnYourNotificationButtonClick(object sender, RoutedEventArgs e)
            => NavigationService.Navigate<Home.NotificationsPage>();
    }
}
