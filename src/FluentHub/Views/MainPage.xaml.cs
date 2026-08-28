// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.ViewModels;
using FluentHub.Utils;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.Graphics;

namespace FluentHub.Views
{
	public sealed partial class MainPage : UserControl
	{
		private readonly MainPageViewModel ViewModel;

		private readonly INavigationService NavigationService;

		private readonly ILogger _logger;

		public MainPage()
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<MainPageViewModel>();
			NavigationService = Ioc.Default.GetRequiredService<INavigationService>();
			_logger = Ioc.Default.GetRequiredService<ILogger>();
		}

		private void OnAddNewTabAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
			=> ExecuteAcceleratorCommand(ViewModel.AddNewTabAcceleratorCommand, args);

		private void OnCloseTabAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
			=> ExecuteAcceleratorCommand(ViewModel.CloseTabAcceleratorCommand, args);

		private void OnNextTabAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
			=> ExecuteAcceleratorCommand(ViewModel.GoToNextTabAcceleratorCommand, args);

		private void OnPreviousTabAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
			=> ExecuteAcceleratorCommand(ViewModel.GoToPreviousTabAcceleratorCommand, args);

		private void OnGoBackAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
			=> ExecuteAcceleratorCommand(ViewModel.GoBackCommand, args);

		private void OnGoForwardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
			=> ExecuteAcceleratorCommand(ViewModel.GoForwardCommand, args);

		private static void ExecuteAcceleratorCommand(System.Windows.Input.ICommand command, KeyboardAcceleratorInvokedEventArgs args)
		{
			if (command.CanExecute(args))
			{
				command.Execute(args);
				args.Handled = true;
			}
		}

		private void SubscribeEvents()
		{
		}

		private void UnsubscribeEvents()
		{
		}

		private InfoBarSeverity UserNotificationToInfoBarSeverity(UserNotificationType type)
		{
			return type switch
			{
				UserNotificationType.Info => InfoBarSeverity.Informational,
				UserNotificationType.Success => InfoBarSeverity.Success,
				UserNotificationType.Warning => InfoBarSeverity.Warning,
				UserNotificationType.Error => InfoBarSeverity.Error,
				_ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
			};
		}

		public async Task InitializeAsync()
		{
			// Initialize the static theme helper to capture a reference to this window
			// to handle theme changes without restarting the app
			ThemeHelpers.Initialize();

			SubscribeEvents();

			NavigationService.Configure(CustomCustomTabView);
			await NavigationService.NavigateAsync(new DashboardRoute());

			var command = ViewModel.LoadSignedInUserCommand;
			if (command.CanExecute(null))
				command.Execute(null);

			// Configure Jump list
			await JumpListHelpers.ConfigureDefaultJumpListAsync();
		}

		public async Task ShutdownAsync()
		{
			UnsubscribeEvents();
			await NavigationService.DisconnectAsync();
		}

		private void OnCustomCustomTabViewLoaded(object sender, RoutedEventArgs e)
		{
			// https://learn.microsoft.com/en-us/windows/apps/develop/title-bar?tabs=winui3#interactive-content
			// It is no longer recognized by the title bar element and its child elements. The rectangular area occupied by the title bar element acts
			// as the title bar for pointer purposes, even if the element is blocked by another element, or the element is transparent.
			// However, keyboard input is recognized and child elements can receive keyboard focus.

			// WINUI3: bad workaround to be removed asap
			// SetDragRectangles() does not work on windows 10 with winappsdk "1.2.220902.1-preview1"
			if (System.Environment.OSVersion.Version.Build >= 22000)
			{
				CustomCustomTabView.DragArea.SizeChanged += (_, _) => SetRectDragRegion();
			}
			else
			{
				MainWindow.Instance.AppWindow.TitleBar.ExtendsContentIntoTitleBar = false;
				MainWindow.Instance.ExtendsContentIntoTitleBar = true;
				MainWindow.Instance.SetTitleBar(CustomCustomTabView.DragArea);
			}
		}

		private void SetRectDragRegion()
		{
			const uint MDT_EFFECTIVE_DPI = 0;

			var displayArea = DisplayArea.GetFromWindowId(MainWindow.Instance.AppWindow.Id, DisplayAreaFallback.Primary);
			var hMonitor = Win32Interop.GetMonitorFromDisplayId(displayArea.DisplayId);

			var hr = NativeWinApiHelper.GetDpiForMonitor(hMonitor, MDT_EFFECTIVE_DPI, out _, out _);
			if (hr != 0)
				return;

			var scaleAdjustment = XamlRoot.RasterizationScale;
			var dragArea = CustomCustomTabView.DragArea;

			var offset = CustomCustomTabView.ActualOffset;

			var x = (int)((offset.X + CustomCustomTabView.ActualWidth - dragArea.ActualWidth) * scaleAdjustment);
			var y = 0;

			var width = (int)(dragArea.ActualWidth * scaleAdjustment);
			var height = (int)(CustomCustomTabView.TitlebarArea.ActualHeight * scaleAdjustment);

			var dragRect = new RectInt32(x, y, width, height);

			// WinUI3: See also, https://github.com/microsoft/WindowsAppSDK/issues/2574
			MainWindow.Instance.AppWindow.TitleBar.SetDragRectangles(new[] { dragRect });
		}

		private void OnTabViewSelectionChanged(object sender, TabViewSelectionChangedEventArgs e)
		{
			RootScreenPresenter.Content = e.NewSelectedItem?.CurrentScreen?.View;
		}

		private void LeftSideNavigationViewOpenerButton_Click(object sender, RoutedEventArgs e)
		{
			LeftSideNavigationView.IsPaneOpen = true;
			LeftSideNavigationView.Visibility = Visibility.Visible;
		}

		private void LeftSideNavigationView_PaneClosed(NavigationView sender, object args)
		{
			LeftSideNavigationView.Visibility = Visibility.Collapsed;
		}

		private async void UserNotificationInBoxButton_Click(object sender, RoutedEventArgs e)
		{
			await NavigationService.NavigateAsync(new NotificationsRoute());
		}

		private async void LeftSideViewerIconMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
		{
			var mfi = (MenuFlyoutItem)sender;
			var login = App.AppSettings.SignedInUserName;

			switch (mfi.Tag.ToString())
			{
				case "YourProfile":
					await NavigationService.NavigateAsync(new UserRoute(login));
					break;
				case "YourRepositories":
					await NavigationService.NavigateAsync(new UserRoute(login, UserSection.Repositories));
					break;
				case "YourProjects":
					await NavigationService.NavigateAsync(new UserRoute(login, UserSection.Projects));
					break;
				case "YourOrganizations":
					await NavigationService.NavigateAsync(new UserRoute(login, UserSection.Organizations));
					break;
				case "YourStars":
					await NavigationService.NavigateAsync(new UserRoute(login, UserSection.Stars));
					break;
				case "AppSettings":
					await NavigationService.NavigateAsync(new AppSettingsRoute());
					break;
				case "AppSignOut":
					App.Current.SignOut();
					break;
			}
		}

		private void LeftSideViewerIconMenuFlyout_Opening(object sender, object e)
		{
			if (!ViewModel.FailedToLoadUserAvatar)
				return;

			var command = ViewModel.LoadSignedInUserCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
