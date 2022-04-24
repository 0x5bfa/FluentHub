using FluentHub.Backend;
using FluentHub.Models;
using FluentHub.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Uwp;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace FluentHub.ViewModels
{
    public class MainPageViewModel : ObservableObject
    {
        public MainPageViewModel(INavigationService navigationService!!,
                                 IMessenger notificationMessenger = null,
                                 ToastService toastService = null,
                                 ILogger logger = null)
        {
            _dispatcher = DispatcherQueue.GetForCurrentThread(); // To Access the UI thread later.
            _navigationService = navigationService;
            _messenger = notificationMessenger;
            _toastService = toastService;
            _logger = logger;

            if (_messenger != null)
                _messenger.Register<UserNotificationMessage>(this, OnNewNotificationReceived);

            AddNewTabAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(AddNewTabAccelerator);
            CloseTabAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(CloseTabAccelerator);
            GoToNextTabAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(GoToNextTabAccelerator);
            GoToPreviousTabAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(GoToPreviousTabAccelerator);
            AddNewTabWithMouseAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(AddNewTabWithMouseAccelerator);
            CloseTabWithMouseAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(CloseTabWithMouseAccelerator);

            GoBackCommand = new RelayCommand(GoBack);
            GoForwardCommand = new RelayCommand(GoForward);
            GoHomeCommand = new RelayCommand(GoHome);
        }

        #region fields
        private readonly DispatcherQueue _dispatcher;
        private readonly INavigationService _navigationService;
        private readonly IMessenger _messenger;
        private readonly ToastService _toastService;
        private readonly ILogger _logger;

        private UserNotificationMessage _lastNotification;
        #endregion

        #region properties
        public UserNotificationMessage LastNotification { get => _lastNotification; private set => SetProperty(ref _lastNotification, value); }
        public static Frame RepositoryContentFrame { get; set; } = new();
        public static Frame PullRequestContentFrame { get; set; } = new();
        #endregion

        #region commands
        public ICommand AddNewTabAcceleratorCommand { get; private set; }
        public ICommand CloseTabAcceleratorCommand { get; private set; }
        public ICommand GoToNextTabAcceleratorCommand { get; private set; }
        public ICommand GoToPreviousTabAcceleratorCommand { get; private set; }
        public ICommand AddNewTabWithMouseAcceleratorCommand { get; private set; }
        public ICommand CloseTabWithMouseAcceleratorCommand { get; private set; }

        public ICommand GoBackCommand { get; private set; }
        public ICommand GoForwardCommand { get; private set; }
        public ICommand GoHomeCommand { get; private set; }
        #endregion

        #region command-methods
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
            _navigationService.Navigate<Views.Home.UserHomePage>();
        }
        #endregion

        private async void OnNewNotificationReceived(object recipient, UserNotificationMessage message)
        {
            // Check if the message method contains the InApp value (multivalue enum)
            if (message.Method.HasFlag(UserNotificationMethod.InApp))
            {
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
    }
}