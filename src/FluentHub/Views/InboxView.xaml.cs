// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views;

public sealed partial class InboxView : UserControl
{
	public InboxView()
	{
		var app = App.Current;
		ViewModel = new InboxViewModel(app.GitHub, app.Session, app.Settings);

		InitializeComponent();
		Loaded += OnInboxViewLoaded;
		Unloaded += OnInboxViewUnloaded;
	}

	public InboxViewModel ViewModel { get; }

	public event Action<InboxView, InboxItemViewModel>? NotificationSelected;

	private async void OnInboxViewLoaded(object sender, RoutedEventArgs e)
		=> await ViewModel.LoadAsync();

	private void OnInboxViewUnloaded(object sender, RoutedEventArgs e)
		=> ViewModel.CancelLoading();

	private void OnNotificationItemClick(object sender, ItemClickEventArgs e)
	{
		if (e.ClickedItem is not InboxItemViewModel item)
			return;

		NotificationSelected?.Invoke(this, item);
	}
}
