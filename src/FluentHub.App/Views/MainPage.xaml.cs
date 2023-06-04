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
		public MainPageViewModel ViewModel { get; }

		public INavigationService NavigationService { get; }

		public ILogger _logger { get; }

		public MainPage()
		{
			InitializeComponent();

			var provider = App.Current.Services;
			ViewModel = provider.GetRequiredService<MainPageViewModel>();
			NavigationService = provider.GetRequiredService<INavigationService>();
			_logger = provider.GetRequiredService<ILogger>();
		}

		#region Methods
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
		#endregion

		#region Event handlers
		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			// Initialize the static theme helper to capture a reference to this window
			// to handle theme changes without restarting the app
			ThemeHelpers.Initialize();

			SubscribeEvents();

			CustomTabViewControl.NewTabPage = typeof(Viewers.UserHomePage);
			NavigationService.Configure(CustomTabViewControl);
			NavigationService.Navigate<Viewers.UserHomePage>();

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
				App.Window.AppWindow.TitleBar.ExtendsContentIntoTitleBar = false;
				App.Window.ExtendsContentIntoTitleBar = true;
				App.Window.SetTitleBar(CustomTabViewControl.DragArea);
			}
		}

		private void SetRectDragRegion()
		{
			const uint MDT_EFFECTIVE_DPI = 0;

			var displayArea = DisplayArea.GetFromWindowId(App.Window.AppWindow.Id, DisplayAreaFallback.Primary);
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
			App.Window.AppWindow.TitleBar.SetDragRectangles(new[] { dragRect });
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
		#endregion

		private void LeftSideViewerIconMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
		{
			var mfi = (MenuFlyoutItem)sender;

			var parameter = NavigationService.TabView.SelectedItem.NavigationBar.Parameter;
			parameter.UserLogin = ViewModel.SignedInUser.Login;
			parameter.AsViewer = false;

			switch (mfi.Tag.ToString())
			{
				case "YourProfile":
					NavigationService.Navigate<Views.Users.OverviewPage>();
					break;
				case "YourRepositories":
					NavigationService.Navigate<Views.Users.RepositoriesPage>();
					break;
				//case "YourOrganizations":
				//	NavigationService.Navigate<Views.Users.OrganizationsPage>();
				//	break;
				case "YourStars":
					NavigationService.Navigate<Views.Users.StarredReposPage>();
					break;
			}
		}
	}
}
