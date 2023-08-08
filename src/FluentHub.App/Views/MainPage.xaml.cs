// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.ViewModels;
using FluentHub.App.Utils;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Windows.Graphics;

namespace FluentHub.App.Views
{
	public sealed partial class MainPage : Page
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

		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			// Initialize the static theme helper to capture a reference to this window
			// to handle theme changes without restarting the app
			ThemeHelpers.Initialize();

			SubscribeEvents();

			CustomTabViewControl.NewTabPage = typeof(Viewers.DashBoardPage);
			NavigationService.Configure(CustomTabViewControl);
			NavigationService.Navigate<Viewers.DashBoardPage>();

			var command = ViewModel.LoadSignedInUserCommand;
			if (command.CanExecute(null))
				command.Execute(null);

			// Configure Jumplist
			await JumpListHelpers.ConfigureDefaultJumpListAsync();
		}

		protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
		{
			UnsubscribeEvents();
			NavigationService.Disconnect();
		}

		private void OnCustomTabViewControlLoaded(object sender, RoutedEventArgs e)
		{
			// https://learn.microsoft.com/en-us/windows/apps/develop/title-bar?tabs=winui3#interactive-content
			// It is no longer recognized by the title bar element and its child elements. The rectangular area occupied by the title bar element acts
			// as the title bar for pointer purposes, even if the element is blocked by another element, or the element is transparent.
			// However, keyboard input is recognized and child elements can receive keyboard focus.

			// WINUI3: bad workaround to be removed asap
			// SetDragRectangles() does not work on windows 10 with winappsdk "1.2.220902.1-preview1"
			if (System.Environment.OSVersion.Version.Build >= 22000)
			{
				CustomTabViewControl.DragArea.SizeChanged += (_, _) => SetRectDragRegion();
			}
			else
			{
				App.WindowInstance.AppWindow.TitleBar.ExtendsContentIntoTitleBar = false;
				App.WindowInstance.ExtendsContentIntoTitleBar = true;
				App.WindowInstance.SetTitleBar(CustomTabViewControl.DragArea);
			}
		}

		private void SetRectDragRegion()
		{
			const uint MDT_EFFECTIVE_DPI = 0;

			var displayArea = DisplayArea.GetFromWindowId(App.WindowInstance.AppWindow.Id, DisplayAreaFallback.Primary);
			var hMonitor = Win32Interop.GetMonitorFromDisplayId(displayArea.DisplayId);

			var hr = NativeWinApiHelper.GetDpiForMonitor(hMonitor, MDT_EFFECTIVE_DPI, out _, out _);
			if (hr != 0)
				return;

			var scaleAdjustment = XamlRoot.RasterizationScale;
			var dragArea = CustomTabViewControl.DragArea;

			var offset = CustomTabViewControl.ActualOffset;

			var x = (int)((offset.X + CustomTabViewControl.ActualWidth - dragArea.ActualWidth) * scaleAdjustment);
			var y = 0;

			var width = (int)(dragArea.ActualWidth * scaleAdjustment);
			var height = (int)(CustomTabViewControl.TitlebarArea.ActualHeight * scaleAdjustment);

			var dragRect = new RectInt32(x, y, width, height);

			// WinUI3: See also, https://github.com/microsoft/WindowsAppSDK/issues/2574
			App.WindowInstance.AppWindow.TitleBar.SetDragRectangles(new[] { dragRect });
		}

		private void OnTabViewSelectionChanged(object sender, TabViewSelectionChangedEventArgs e)
		{
			RootFrameBorder.Content = e.NewSelectedItem?.Frame;
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

		private void UserNotificationInBoxButton_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate<Viewers.NotificationsPage>();
		}

		private void LeftSideViewerIconMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
		{
			var mfi = (MenuFlyoutItem)sender;

			var parameter = NavigationService.TabView.SelectedItem.NavigationBar.Context;

			var navBar = NavigationService.TabView.SelectedItem.NavigationBar;
			navBar.Context = new()
			{
				PrimaryText = ViewModel.SignedInUser.Login,
				AsViewer = false,
			};

			switch (mfi.Tag.ToString())
			{
				case "YourProfile":
					NavigationService.Navigate<Users.OverviewPage>();
					break;
				case "YourRepositories":
					NavigationService.Navigate<Users.RepositoriesPage>();
					break;
				//case "YourOrganizations":
				//	NavigationService.Navigate<Views.Users.OrganizationsPage>();
				//	break;
				case "YourStars":
					NavigationService.Navigate<Users.StarsPage>();
					break;
			}
		}
	}
}
