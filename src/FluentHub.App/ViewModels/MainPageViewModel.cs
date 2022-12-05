using CommunityToolkit.WinUI.UI;
using FluentHub.App.Services;
using FluentHub.App.Models;
using FluentHub.App.Utils;
using Microsoft.UI.Xaml.Input;
using System.Windows.Input;
using Windows.System;

namespace FluentHub.App.ViewModels
{
    public class MainPageViewModel : ObservableObject
    {
        public MainPageViewModel(INavigationService navigationService, IMessenger notificationMessenger = null, ToastService toastService = null, ILogger logger = null)
        {
            // To Access the UI thread later.
            _dispatcher = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();

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

            _autoSuggestionItems = new();
            AutoSuggestionItems = new(_autoSuggestionItems);

            _navViewFooterItems = new();
            NavViewFooterItems = new(_navViewFooterItems);

            _navViewItems.Add(new (name: "Home", glyphPrimary: "\uE80F", glyphSecondary: "\uEA8A", isSelected: true, image: new(new Uri("ms-appx:///Assets/Icons/Home.png"))));
            _navViewItems.Add(new (name: "Notifications", glyphPrimary: "\uEA8F", glyphSecondary: "\uEA8F", image: new(new Uri("ms-appx:///Assets/Icons/Notifications.png"))));
            _navViewItems.Add(new (name: "Activity", glyphPrimary: "\uECAD", glyphSecondary: "\uECAD", image: new(new Uri("ms-appx:///Assets/Icons/Activities.png"))));
            //_navViewItems.Add(new (name: "Marketplace", glyphPrimary: "\uE14D", glyphSecondary: "\uE14D", image: new(new Uri("ms-appx:///Assets/Icons/Marketplace.png"))));
            //_navViewItems.Add(new (name: "Explore", glyphPrimary: "\uE805", glyphSecondary: "\uE805", image: new(new Uri("ms-appx:///Assets/Icons/Explore.png"))));
            _navViewFooterItems.Add(new (name: "Profile", glyphPrimary: "\uE77B", glyphSecondary: "\uEA8C", image: new(new Uri("ms-appx:///Assets/Icons/Profile.png"))));

            AddNewTabAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(AddNewTabAccelerator);
            CloseTabAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(CloseTabAccelerator);
            GoToNextTabAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(GoToNextTabAccelerator);
            GoToPreviousTabAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(GoToPreviousTabAccelerator);
            AddNewTabWithMouseAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(AddNewTabWithMouseAccelerator);
            CloseTabWithMouseAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(CloseTabWithMouseAccelerator);

            GoBackCommand = new RelayCommand(GoBack);
            GoForwardCommand = new RelayCommand(GoForward);

            LoadSignedInUserCommand = new AsyncRelayCommand(LoadSignedInUserAsync);
        }

        #region Fields and Properties
        private readonly Microsoft.UI.Dispatching.DispatcherQueue _dispatcher;
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

        private string _searchTerm;
        public string SearchTerm { get => _searchTerm; set => SetProperty(ref _searchTerm, value); }

        private readonly ObservableCollection<SearchQueryModel> _autoSuggestionItems;
        public ReadOnlyObservableCollection<SearchQueryModel> AutoSuggestionItems;

        private readonly ObservableCollection<SquareNavigationViewItemModel> _navViewItems;
        public ReadOnlyObservableCollection<SquareNavigationViewItemModel> NavViewItems;

        private readonly ObservableCollection<SquareNavigationViewItemModel> _navViewFooterItems;
        public ReadOnlyObservableCollection<SquareNavigationViewItemModel> NavViewFooterItems;
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

        public IAsyncRelayCommand LoadSignedInUserCommand { get; }
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

        private async Task LoadSignedInUserAsync()
        {
            // This task should not be notified whether task fault,
            // as it is not user-triggered task.

            //_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
            //bool faulted = false;

            string _currentTaskingMethodName = nameof(LoadSignedInUserAsync);

            try
            {
                Octokit.Queries.Users.UserQueries queries = new();
                var user = await queries.GetAsync(App.AppSettings.SignedInUserName);

                SignedInUser = user ?? new();

                Octokit.Queries.Users.NotificationQueries notificationQueries = new();
                var count = await notificationQueries.GetUnreadCount();

                UnreadCount = count;
                _toastService?.UpdateBadgeGlyph(BadgeGlyphType.Number, UnreadCount);
            }
            catch (Exception ex)
            {
                //TaskException = ex;
                //faulted = true;

                _logger?.Error(_currentTaskingMethodName, ex);
                throw;
            }
            finally
            {
                //_messenger?.Send(new TaskStateMessaging(faulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
            }
        }
        #endregion

        public void ClearSearchQueryModelItems()
            => _autoSuggestionItems.Clear();

        public void AddNewSearchQueryModel(string query, string label)
            => _autoSuggestionItems.Add(new(query, label));

        private void OnNewNotificationReceived(object recipient, UserNotificationMessage message)
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

                // [Obsolete]
                //// Show the message in the UI
                //// using the dispatcher to access the UI thread
                //_dispatcher.TryEnqueue(() => LastNotification = message);

                //// Show the message in the app
                //_logger?.Info("InApp notification received: {0}", message);
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
    }
}
