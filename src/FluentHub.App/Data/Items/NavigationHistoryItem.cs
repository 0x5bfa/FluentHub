// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.Data.Items
{
	public class NavigationHistoryItem : ObservableObject
	{
		private IconSource? _Icon;
		public IconSource? Icon { get => _Icon; set => SetProperty(ref _Icon, value); }

		private string? _Header;
		public string? Header { get => _Header; set => SetProperty(ref _Header, value); }

		private string? _Description;
		public string? Description { get => _Description; set => SetProperty(ref _Description, value); }

		public FrameNavigationParameter? Context { get; set; }

		public NavigationPageKind PageKind { get; set; }

		public NavigationPageKey PageKey { get; set; }
	}
}
