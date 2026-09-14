// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views
{
	public sealed partial class RootView : UserControl
	{
		private bool _repositoryItemsAdded;

		public RootView()
		{
			var app = App.Current;
			ViewModel = new RootViewModel(app.GitHub, app.Session, app.Settings);

			InitializeComponent();
			Loaded += OnRootViewLoaded;
			Unloaded += OnRootViewUnloaded;
		}

		public RootViewModel ViewModel { get; }

		private async void OnRootViewLoaded(object sender, RoutedEventArgs e)
		{
			MainWindow.Instance.SetTitleBar(TitleBar);

			if (_repositoryItemsAdded)
				return;

			try
			{
				await ViewModel.LoadRepositoriesAsync();

				foreach (var repository in ViewModel.Repositories)
				{
					NavigationView.MenuItems.Add(new NavigationViewItem
					{
						Content = repository.Name,
						Tag = repository.FullName,
						Icon = new FontIcon { Glyph = repository.IconGlyph },
					});
				}

				_repositoryItemsAdded = true;
			}
			catch (OperationCanceledException)
			{
				// The view was unloaded while the request was in flight.
			}
			catch (Exception)
			{
				// Keep the navigation shell usable when GitHub is unavailable.
			}
		}

		private void OnRootViewUnloaded(object sender, RoutedEventArgs e)
			=> ViewModel.CancelLoading();

		private void OnNavigationSelectionChanged(
			NavigationView sender,
			NavigationViewSelectionChangedEventArgs args)
		{
			if (args.SelectedItem is not NavigationViewItem item)
				return;

			ContentFrame.Content = new TextBlock
			{
				Text = item.Tag?.ToString() ?? item.Content?.ToString(),
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
			};
		}
	}
}
