// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Utils;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Data.Items
{
	public class TabViewItem : ObservableObject, ITabViewItem
	{
		private readonly ILogger _logger;

		public Guid Guid { get; }

		public Frame Frame { get; }

		public NavigationHistory NavigationHistory { get; }

		public NavigationBarModel NavigationBar { get; }

		public TabViewItem()
		{
			// Dependency injection
			_logger = Ioc.Default.GetRequiredService<ILogger>();

			// Initialize
			Guid = Guid.NewGuid();
			Frame = new();
			NavigationHistory = new();
			NavigationBar = new();
			NavigationBar.NavigationBarItems = new();

			Frame.Navigating += OnFrameNavigating;
		}

		private void OnFrameNavigating(object sender, NavigatingCancelEventArgs e)
		{
			switch (e.NavigationMode)
			{
				case NavigationMode.New:
					NavigationHistory.NavigateTo(new NavigationHistoryItem(), NavigationHistory.CurrentItemIndex + 1);
					break;
				case NavigationMode.Back:
					NavigationHistory.GoBack();
					break;
				case NavigationMode.Forward:
					NavigationHistory.GoForward();
					break;
			}

			_logger?.Info("ITabViewItem.OnFrameNavigating [Page: {0}, Parameter: {1}, NavigationMode: {2}]", e.SourcePageType, e.Parameter, e.NavigationMode);
		}
	}
}
