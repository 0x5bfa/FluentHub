using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.IO;
using Windows.System;
using Windows.Storage;

namespace FluentHub.Uwp.UserControls
{
    public sealed partial class LoadErrorDisplay : UserControl
    {
        #region propdp
        public static readonly DependencyProperty TaskExceptionProperty =
            DependencyProperty.Register(
                nameof(TaskException),
                typeof(Exception),
                typeof(LoadErrorDisplay),
                new PropertyMetadata(null)
                );

        public Exception TaskException
        {
            get => (Exception)GetValue(TaskExceptionProperty);
            set
            {
                SetValue(TaskExceptionProperty, value);
                NotifyErrorContent();
            }
        }

        public static readonly DependencyProperty ActionProperty =
            DependencyProperty.Register(
                nameof(Action),
                typeof(FrameworkElement),
                typeof(LoadErrorDisplay),
                new PropertyMetadata(null)
                );

        public FrameworkElement Action
        {
            get => (FrameworkElement)GetValue(ActionProperty);
            set => SetValue(ActionProperty, value);
        }
        #endregion

        public LoadErrorDisplay()
        {
            SetCurrentTabItem();

            InitializeComponent();
        }

        private async void OnShowExceptionDetailsContentDialogButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new Dialogs.ExceptionStackTraceDialog(TaskException);
            _ = await dialog.ShowAsync();
        }

        private async void OnOpenLogFileLocationButtonClick(object sender, RoutedEventArgs e)
        {
            var provider = App.Current.Services;
            var logger = provider.GetRequiredService<Utils.ILogger>();

            string logsFolder = Path.Combine(ApplicationData.Current.LocalFolder.Path, "FluentHub.Logs");
            var result = await Launcher.LaunchFolderPathAsync(logsFolder);
            logger?.Info("Open logs folder result: {0}", result);
        }

        private async void OnReportThisIssueButtonClick(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://github.com/FluentHub/FluentHub/issues/new/choose"));
        }

        private void SetCurrentTabItem()
        {
            var provider = App.Current.Services;
            INavigationService navigationService = provider.GetRequiredService<INavigationService>();

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Something went wrong";
            currentItem.Description = "Something went wrong";
        }

        private void  NotifyErrorContent()
        {
            var provider = App.Current.Services;
            var messenger = provider.GetRequiredService<IMessenger>();

            UserNotificationMessage notification = new("Something went wrong", TaskException?.Message, UserNotificationType.Error);
            messenger?.Send(notification);
        }
    }
}
