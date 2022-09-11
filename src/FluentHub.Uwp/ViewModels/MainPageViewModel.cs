using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using Microsoft.Toolkit.Uwp;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace FluentHub.Uwp.ViewModels
{
    public class MainPageViewModel : ObservableObject
    {
        public MainPageViewModel(INavigationService navigationService, IMessenger notificationMessenger = null, ToastService toastService = null, ILogger logger = null)
        {
            _dispatcher = DispatcherQueue.GetForCurrentThread(); // To Access the UI thread later.
            _navigationService = navigationService;
            _messenger = notificationMessenger;
            _toastService = toastService;
            _logger = logger;

            if (_messenger != null)
            {
                _messenger.Register<UserNotificationMessage>(this, OnNewNotificationReceived);
                _messenger.Register<TaskStateMessaging>(this, OnIfContentIsLoadingRecieved);
            }

            _navViewItems = new();
            NavViewItems = new(_navViewItems);

            _navViewFooterItems = new();
            NavViewFooterItems = new(_navViewFooterItems);

            _navViewItems.Add(new (name: "Home", glyphPrimary: "\uE80F", glyphSecondary: "\uEA8A", isSelected: true));
            _navViewItems.Add(new (name: "Notifications", glyphPrimary: "\uEA8F", glyphSecondary: "\uEA8F"));
            _navViewItems.Add(new (name: "Activity", glyphPrimary: "\uECAD", glyphSecondary: "\uECAD"));
            _navViewItems.Add(new (name: "Marketplace", glyphPrimary: "\uE14D", glyphSecondary: "\uE14D"));
            _navViewItems.Add(new (name: "Explore", glyphPrimary: "\uE805", glyphSecondary: "\uE805"));
            _navViewFooterItems.Add(new (name: "Profile", glyphPrimary: "\uE77B", glyphSecondary: "\uEA8C"));

            AddNewTabAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(AddNewTabAccelerator);
            CloseTabAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(CloseTabAccelerator);
            GoToNextTabAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(GoToNextTabAccelerator);
            GoToPreviousTabAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(GoToPreviousTabAccelerator);
            AddNewTabWithMouseAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(AddNewTabWithMouseAccelerator);
            CloseTabWithMouseAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(CloseTabWithMouseAccelerator);

            GoBackCommand = new RelayCommand(GoBack);
            GoForwardCommand = new RelayCommand(GoForward);

            GoHomeCommand = new RelayCommand(GoHome);
            GoNotificationsCommand = new RelayCommand(GoNotifications);
            GoActivitiesCommand = new RelayCommand(GoActivities);
            GoExplorerCommand = new RelayCommand(GoExplorer);
            GoMarketplaceCommand = new RelayCommand(GoMarketplace);
            GoProfileCommand = new RelayCommand(GoProfile);

            LoadSignedInUserCommand = new AsyncRelayCommand(LoadSignedInUserAsync);
        }

        #region Fields and Properties
        private readonly DispatcherQueue _dispatcher;
        private readonly INavigationService _navigationService;
        private readonly IMessenger _messenger;
        private readonly ToastService _toastService;
        private readonly ILogger _logger;

        private UserNotificationMessage _lastNotification;
        public UserNotificationMessage LastNotification { get => _lastNotification; private set => SetProperty(ref _lastNotification, value); }

        private Octokit.Models.v4.User _signedInUser;
        public Octokit.Models.v4.User SignedInUser { get => _signedInUser; private set => SetProperty(ref _signedInUser, value); }

        private bool _taskIsInProgress;
        public bool TaskIsInProgress { get => _taskIsInProgress; private set => SetProperty(ref _taskIsInProgress, value); }

        private bool _taskIsCompletedSuccessfully;
        public bool TaskIsCompletedSuccessfully { get => _taskIsCompletedSuccessfully; private set => SetProperty(ref _taskIsCompletedSuccessfully, value); }

        private int _unreadCount;
        public int UnreadCount { get => _unreadCount; private set => SetProperty(ref _unreadCount, value); }

        public static Frame RepositoryContentFrame { get; set; } = new();
        public static Frame PullRequestContentFrame { get; set; } = new();

        private bool _isHome;
        public bool IsHome { get => _isHome; private set => SetProperty(ref _isHome, value); }

        private bool _isNotifications;
        public bool IsNotifications { get => _isNotifications; private set => SetProperty(ref _isNotifications, value); }

        private bool _isActivities;
        public bool IsActivities { get => _isActivities; private set => SetProperty(ref _isActivities, value); }

        private bool _isExplorer;
        public bool IsExplorer { get => _isExplorer; private set => SetProperty(ref _isExplorer, value); }

        private bool _isMarketplace;
        public bool IsMarketplace { get => _isMarketplace; private set => SetProperty(ref _isMarketplace, value); }

        private bool _isProfile;
        public bool IsProfile { get => _isProfile; private set => SetProperty(ref _isProfile, value); }

        private readonly ObservableCollection<SquareNavigationViewItemModel> _navViewItems;
        public ReadOnlyObservableCollection<SquareNavigationViewItemModel> NavViewItems;

        private readonly ObservableCollection<SquareNavigationViewItemModel> _navViewFooterItems;
        public ReadOnlyObservableCollection<SquareNavigationViewItemModel> NavViewFooterItems;

        public IAsyncRelayCommand LoadSignedInUserCommand { get; }
        #endregion

        #region Commands
        public ICommand AddNewTabAcceleratorCommand { get; private set; }
        public ICommand CloseTabAcceleratorCommand { get; private set; }
        public ICommand GoToNextTabAcceleratorCommand { get; private set; }
        public ICommand GoToPreviousTabAcceleratorCommand { get; private set; }
        public ICommand AddNewTabWithMouseAcceleratorCommand { get; private set; }
        public ICommand CloseTabWithMouseAcceleratorCommand { get; private set; }

        public ICommand GoBackCommand { get; private set; }
        public ICommand GoForwardCommand { get; private set; }

        public ICommand GoHomeCommand { get; private set; }
        public ICommand GoNotificationsCommand { get; private set; }
        public ICommand GoActivitiesCommand { get; private set; }
        public ICommand GoExplorerCommand { get; private set; }
        public ICommand GoMarketplaceCommand { get; private set; }
        public ICommand GoProfileCommand { get; private set; }
        #endregion

        #region Command methods
        private void AddNewTabAccelerator(KeyboardAcceleratorInvokedEventArgs e)
        {
            _navigationService.OpenTab<Views.Home.UserHomePage>();
            e.Handled = true;
        }

        private void CloseTabAccelerator(KeyboardAcceleratorInvokedEventArgs e)
        {
            _navigationService.CloseTab(_navigationService.TabView.SelectedItem.Guid);
            e.Handled = true;
        }

        private void GoToNextTabAccelerator(KeyboardAcceleratorInvokedEventArgs e)
        {
            if (_navigationService.TabView.SelectedIndex == _navigationService.TabView.Items.Count - 1)
            {
                _navigationService.TabView.SelectedIndex = 0;
            }
            else
            {
                _navigationService.TabView.SelectedIndex++;
            }

            e.Handled = true;
        }

        private void GoToPreviousTabAccelerator(KeyboardAcceleratorInvokedEventArgs e)
        {
            if (_navigationService.TabView.SelectedIndex == _navigationService.TabView.Items.Count - 1)
            {
                _navigationService.TabView.SelectedIndex = 0;
            }
            else
            {
                _navigationService.TabView.SelectedIndex--;
            }

            e.Handled = true;
        }

        private void AddNewTabWithMouseAccelerator(KeyboardAcceleratorInvokedEventArgs e)
        {
            _navigationService.OpenTab<Views.Home.UserHomePage>();
            e.Handled = true;
        }

        private void CloseTabWithMouseAccelerator(KeyboardAcceleratorInvokedEventArgs e)
        {
            _navigationService.CloseTab(_navigationService.TabView.SelectedItem.Guid);
            e.Handled = true;
        }

        private void GoBack()
        {
            _navigationService.GoBack();
        }

        private void GoForward()
        {
            _navigationService.GoForward();
        }

        private void GoHome()
        {
            InitializeToggles();
            IsHome = true;
            _navigationService.Navigate<Views.Home.UserHomePage>();
        }

        private void GoNotifications()
        {
            InitializeToggles();
            IsNotifications = true;
            _navigationService.Navigate<Views.Home.NotificationsPage>();
        }

        private void GoActivities()
        {
            InitializeToggles();
            IsActivities = true;
            _navigationService.Navigate<Views.Home.ActivitiesPage>();
        }

        private void GoExplorer()
        {
            InitializeToggles();
            IsExplorer = true;
            //_navigationService.Navigate<Views.Home.UserHomePage>();
        }

        private void GoMarketplace()
        {
            InitializeToggles();
            IsMarketplace = true;
            //_navigationService.Navigate<Views.Home.UserHomePage>();
        }

        private void GoProfile()
        {
            InitializeToggles();
            IsProfile = true;
            _navigationService.Navigate<Views.Users.OverviewPage>(
                new FrameNavigationArgs()
                {
                    Login = App.Settings.SignedInUserName,
                });
        }

        private void InitializeToggles()
        {
            IsHome = false;
            IsNotifications = false;
            IsActivities = false;
            IsExplorer = false;
            IsMarketplace = false;
            IsProfile = false;
        }
        #endregion

        private async void OnNewNotificationReceived(object recipient, UserNotificationMessage message)
        {
            // Check if the message method contains the InApp value (multivalue enum)
            if (message.Method.HasFlag(UserNotificationMethod.InApp))
            {
                // Thrown by Home.NotificationsViewModel
                if (message.Title == "NotificationCount")
                {
                    UnreadCount = Convert.ToInt32(message.Message);
                    return;
                }

                // Show the message in the UI
                // using the dispatcher to access the UI thread
                await _dispatcher.EnqueueAsync(() => LastNotification = message);
                // Show the message in the app
                _logger?.Info("InApp notification received: {0}", message);
            }

            // Check if the message method contains the Toast value (multivalue enum)
            if (message.Method.HasFlag(UserNotificationMethod.Toast))
            {
                _toastService?.ShowToastNotification(message.Title, message.Message);
                // Show the message in the toast
                _logger?.Info("Toast notification received: {0}", message);
            }
        }

        private void OnIfContentIsLoadingRecieved(object recipient, TaskStateMessaging message)
        {
            switch (message.StatusType)
            {
                case TaskStatusType.IsStarted:
                    TaskIsInProgress = true;
                    break;
                case TaskStatusType.IsCompleted:
                    TaskIsInProgress = false;
                    break;
                case TaskStatusType.IsCompletedSuccessfully:
                    TaskIsCompletedSuccessfully = true;
                    TaskIsInProgress = false;
                    break;
                case TaskStatusType.IsFaulted:
                    TaskIsCompletedSuccessfully = false;
                    TaskIsInProgress = false;
                    break;
            }
        }

        private async Task LoadSignedInUserAsync()
        {
            try
            {
                Octokit.Queries.Users.UserQueries queries = new();
                var user = await queries.GetAsync(App.Settings.SignedInUserName);

                SignedInUser = user ?? new();

                Octokit.Queries.Users.NotificationQueries notificationQueries = new();
                var count = await notificationQueries.GetUnreadCount();

                UnreadCount = count;
                _toastService?.UpdateBadgeGlyph(BadgeGlyphType.Number, UnreadCount);
            }
            catch (Exception ex)
            {
                _logger?.Error("MainPageViewModel.GetSignedInUser(): ", ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }
    }
}
