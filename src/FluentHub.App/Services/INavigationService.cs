// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace FluentHub.App.Services
{
	public interface INavigationService
	{
		Type CurrentPage { get; set; }

		ITabView TabView { get; }

		bool IsConfigured { get; }

		void Configure(ITabView tabView);

		void Disconnect();

		void Navigate(Type page, object parameter = null, NavigationTransitionInfo transitionInfo = null);

		void Navigate<T>(object parameter = null, NavigationTransitionInfo transitionInfo = null) where T : Page;

		Guid OpenTab(Type page, object parameter = null);

		Guid OpenTab<T>(object parameter = null) where T : Page;

		void CloseTab(Guid tabId);

		void GoToTab(Guid tabId);

		void GoBack();

		void GoForward();

		void Reload();
	}
}
