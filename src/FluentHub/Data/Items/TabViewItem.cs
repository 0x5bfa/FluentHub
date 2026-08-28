// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Utils;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Data.Items
{
	public class TabViewItem : ObservableObject, ITabViewItem
	{
		private readonly ILogger _logger;
		private NavigationHistorySnapshot? _pendingNavigation;

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
			NavigationBar = new();
			NavigationBar.NavigationBarItems = new();
			NavigationHistory = new(NavigationBar);

			Frame.Navigating += OnFrameNavigating;
			Frame.Navigated += OnFrameNavigated;
			Frame.NavigationFailed += OnFrameNavigationFailed;
			Frame.NavigationStopped += OnFrameNavigationStopped;
		}

		private void OnFrameNavigating(object sender, NavigatingCancelEventArgs e)
		{
			_pendingNavigation = NavigationHistory.CaptureSnapshot();

			try
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
			}
			catch
			{
				RollbackPendingNavigation();
				throw;
			}

			_logger?.Info("ITabViewItem.OnFrameNavigating [Page: {0}, Parameter: {1}, NavigationMode: {2}]", e.SourcePageType, e.Parameter, e.NavigationMode);
		}

		private void OnFrameNavigated(object sender, NavigationEventArgs e)
			=> _pendingNavigation = null;

		private void OnFrameNavigationFailed(object sender, NavigationFailedEventArgs e)
			=> RollbackPendingNavigation();

		private void OnFrameNavigationStopped(object sender, NavigationEventArgs e)
			=> RollbackPendingNavigation();

		private void RollbackPendingNavigation()
		{
			if (_pendingNavigation is null)
				return;

			NavigationHistory.RestoreSnapshot(_pendingNavigation);
			_pendingNavigation = null;
		}
	}
}
