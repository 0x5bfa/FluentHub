using FluentHub.App.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.IO;
using Windows.Storage;
using Windows.System;

namespace FluentHub.App.UserControls
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

			// https://github.com/microsoft/microsoft-ui-xaml/issues/2504
			dialog.XamlRoot = this.Content.XamlRoot;

			_ = await dialog.ShowAsync();
		}

		private async void OnOpenLogFileLocationButtonClick(object sender, RoutedEventArgs e)
		{
			var logger = Ioc.Default.GetRequiredService<Utils.ILogger>();

			string logsFolder = Path.Combine(ApplicationData.Current.LocalFolder.Path, "FluentHub.Logs");
			var result = await Launcher.LaunchFolderPathAsync(logsFolder);
			logger?.Info("Open logs folder result: {0}", result);
		}

		private async void OnReportThisIssueButtonClick(object sender, RoutedEventArgs e)
		{
			await Launcher.LaunchUriAsync(new Uri("https://github.com/FluentHub/FluentHub/issues/new?assignees=0x5bfa&labels=bug&projects=&template=bug_report.yml"));
		}

		private void SetCurrentTabItem()
		{
			INavigationService navigationService = Ioc.Default.GetRequiredService<INavigationService>();

			var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
			currentItem.Header = "Something went wrong";
			currentItem.Description = "Something went wrong";
		}

		private void NotifyErrorContent()
		{
			var messenger = Ioc.Default.GetRequiredService<IMessenger>();

			UserNotificationMessage notification = new("Something went wrong", TaskException?.Message, UserNotificationType.Error);
			messenger?.Send(notification);
		}
	}
}
