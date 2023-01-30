using FluentHub.App.Dialogs;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Services.Navigation;
using FluentHub.App.ViewModels;
using FluentHub.App.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using Windows.ApplicationModel.Core;
using Windows.Graphics;
using Windows.System;
using Windows.UI.Core;

namespace FluentHub.App.Views
{
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			InitializeComponent();

			var provider = App.Current.Services;
			ViewModel = provider.GetRequiredService<MainPageViewModel>();
			navService = provider.GetRequiredService<INavigationService>();
			Logger = provider.GetService<ILogger>();
		}

		#region Fields and Properties
		public MainPageViewModel ViewModel { get; }
		private INavigationService navService { get; }
		public ILogger Logger { get; }
		#endregion

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

			CustomTabViewControl.NewTabPage = typeof(Home.UserHomePage);
			navService.Configure(CustomTabViewControl);
			navService.Navigate<Home.UserHomePage>();

			var command = ViewModel.LoadSignedInUserCommand;
			if (command.CanExecute(null))
				command.Execute(null);

			// Configure Jumplist
			await JumpListHelpers.ConfigureDefaultJumpListAsync();
		}

		protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
		{
			UnsubscribeEvents();
			navService.Disconnect();
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
			const uint MDT_Effective_DPI = 0;

			var displayArea = DisplayArea.GetFromWindowId(App.Window.AppWindow.Id, DisplayAreaFallback.Primary);
			var hMonitor = Win32Interop.GetMonitorFromDisplayId(displayArea.DisplayId);
			var hr = NativeWinApiHelper.GetDpiForMonitor(hMonitor, MDT_Effective_DPI, out var dpiX, out _);
			if (hr != 0)
				return;

			var scaleAdjustment = XamlRoot.RasterizationScale;
			var dragArea = CustomTabViewControl.DragArea;

			var offset = CustomTabViewControl.ActualOffset;

			var x = (int)(((double)offset.X + CustomTabViewControl.ActualWidth - dragArea.ActualWidth) * scaleAdjustment);
			var y = 0;
			var width = (int)(dragArea.ActualWidth * scaleAdjustment);
			var height = (int)(CustomTabViewControl.TitlebarArea.ActualHeight * scaleAdjustment);

			var titlebarRect = new RectInt32((int)MainPageTitleBar.ActualOffset.X, (int)MainPageTitleBar.ActualOffset.Y, (int)(MainPageTitleBar.ActualWidth + MainPageTitleBar.Margin.Left + MainPageTitleBar.Margin.Right), 44);
			var dragRect = new RectInt32(x, y, width, height);

			// WinUI3: Need to track this issue https://github.com/microsoft/WindowsAppSDK/issues/2574
			// They is not fixed in latest stable version v1.5 However, it has been fixed in latest preview version v1.2.220930.4-preview2
			// So we have no choice but to use unstable version of WASDK. Should use latest stable version.
			App.Window.AppWindow.TitleBar.SetDragRectangles(new[] { titlebarRect, dragRect });
		}

		private void OnSearchGitHubButtonButtonClick(object sender, RoutedEventArgs e)
		{
			SearchGitHubAutoSuggestBox.Visibility = Visibility.Visible;
			SearchGitHubAutoSuggestBox.Focus(FocusState.Pointer);
			SearchGitHubButton.Visibility = Visibility.Collapsed;

			if (ViewModel.SearchTerm != null)
				SearchGitHubAutoSuggestBox.Text = ViewModel.SearchTerm;
		}

		//private void OpenSearchAccelerator(KeyboardAcceleratorInvokedEventArgs e)
		//{
		//    SearchGitHubAutoSuggestBox.Visibility = Visibility.Visible;
		//    SearchGitHubAutoSuggestBox.Focus(FocusState.Keyboard);
		//    SearchGitHubButton.Visibility = Visibility.Collapsed;
		//}

		private void OnSearchGitHubAutoSuggestBoxLostFocus(object sender, RoutedEventArgs e)
		{
			//Type currentPage = navService.CurrentPage;
			//var currentPageNamespace = currentPage.ToString();
			//if (!currentPageNamespace.Contains("Views.Searches"))
			//{
				SearchGitHubAutoSuggestBox.Visibility = Visibility.Collapsed;
				SearchGitHubButton.Visibility = Visibility.Visible;
			//}
		}

		private void OnSearchGitHubAutoSuggestBoxQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
		{
			string queryTerm;

			// Suggestion chosen
			if (args.ChosenSuggestion != null)
			{
				queryTerm = (args.ChosenSuggestion as SearchQueryModel).QueryString;
			}
			else
			{
				queryTerm = args.QueryText;
			}

			ViewModel.SearchTerm = queryTerm;

			sender.Text = queryTerm;
			navService.Navigate<Searches.RepositoriesPage>(new Models.FrameNavigationArgs() { Parameters = new() { queryTerm } });
		}

		private void OnSearchGitHubAutoSuggestBoxSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
		{
			var selectedItem = args.SelectedItem as SearchQueryModel;
			sender.Text = selectedItem.QueryString;
		}

		private void OnSearchGitHubAutoSuggestBoxTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
		{
			// Only get results when it was a user typing, 
			// otherwise assume the value got filled in by TextMemberPath 
			// or the handler for SuggestionChosen.
			if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
			{
				var suitableItems = new List<string>();

				ViewModel.SearchTerm = sender.Text;

				ViewModel.ClearSearchQueryModelItems();

				bool isInRepositories = false;
				bool isInOrganizations = false;
				bool isInUsers = false;

				Type currentPage = navService.CurrentPage;
				var currentPageNamespace = currentPage.ToString();

				if (sender.Text != "")
				{
					ViewModel.AddNewSearchQueryModel(sender.Text, "All GitHub");
				}

				if (currentPageNamespace.Contains("Views.Organizations"))
				{
					ViewModel.AddNewSearchQueryModel(sender.Text, "in this organization");
				}
				else if (currentPageNamespace.Contains("Views.Repositories"))
				{
					ViewModel.AddNewSearchQueryModel(sender.Text, "in this repository");
				}
				else if (currentPageNamespace.Contains("Views.Users"))
				{
					ViewModel.AddNewSearchQueryModel(sender.Text, "in this user");
				}
			}
		}

		private void OnTabViewSelectionChanged(object sender, TabViewSelectionChangedEventArgs e)
		{
			RootFrameBorder.Content = e.NewSelectedItem?.Frame;
		}

		private void OnMainNavViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
		{
			switch (args.InvokedItemContainer.Tag.ToString().ToLower())
			{
				default:
				case "home":
					navService.Navigate<Home.UserHomePage>();
					break;
				case "notifications":
					navService.Navigate<Home.NotificationsPage>();
					break;
				case "activities":
					navService.Navigate<Users.ContributionsPage>();
					break;
				case "profile":
					navService.Navigate<Users.OverviewPage>(
					new FrameNavigationArgs()
					{
						Login = App.AppSettings.SignedInUserName,
					});
					break;
				case "profile":
					navService.Navigate<AppSettings.AppearancePage>();
					break;
			}
		}
		#endregion
	}
}
