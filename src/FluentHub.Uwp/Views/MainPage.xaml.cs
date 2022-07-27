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

        private muxc.InfoBarSeverity UserNotificationToInfoBarSeverity(UserNotificationType type)
        {
            return type switch
            {
                UserNotificationType.Info => muxc.InfoBarSeverity.Informational,
                UserNotificationType.Success => muxc.InfoBarSeverity.Success,
                UserNotificationType.Warning => muxc.InfoBarSeverity.Warning,
                UserNotificationType.Error => muxc.InfoBarSeverity.Error,
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

        private void OnDragAreaLoaded(object sender, RoutedEventArgs e)
            => Window.Current.SetTitleBar(DragArea);

        private void OnTabViewSelectionChanged(object sender, TabViewSelectionChangedEventArgs e)
            => RootFrameBorder.Child = e.NewSelectedItem?.Frame;

        private void OnMenuFlyoutItemClick(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuFlyoutItem;

            switch (item?.Tag)
            {
                case "NewRepo":
                    break;
                case "NewOrganization":
                    break;
                case "Profile":
                    NavigationService.Navigate<Users.OverviewPage>(
                        new FrameNavigationArgs()
                        {
                            Login = App.Settings.SignedInUserName,
                        });
                    break;
                case "Repositories":
                    NavigationService.Navigate<Users.RepositoriesPage>("fluenthub://repositories");
                    break;
                case "Discussions":
                    NavigationService.Navigate<Users.DiscussionsPage>("fluenthub://discussions");
                    break;
                case "Issues":
                    NavigationService.Navigate<Users.IssuesPage>("https://github.com/issues");
                    break;
                case "PullRequests":
                    NavigationService.Navigate<Users.PullRequestsPage>("https://github.com/pulls");
                    break;
                case "Organizations":
                    NavigationService.Navigate<Users.OrganizationsPage>("fluenthub://organizations");
                    break;
                case "Stars":
                    NavigationService.Navigate<Users.StarredReposPage>("fluenthub://stars");
                    break;
                case "AccountSettings":
                    NavigationService.Navigate<AppSettings.MainSettingsPage>("fluenthub://settings/account");
                    break;
                case "Settings":
                    NavigationService.Navigate<AppSettings.MainSettingsPage>("fluenthub://settings");
                    break;
                case "SignOut":
                    Frame rootFrame = (Frame)Window.Current.Content;
                    rootFrame.Navigate(typeof(SignIn.IntroPage));
                    break;
            }
        }
        #endregion
    }
}
