// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.Data.Items
{
	public class PageNavigationEntry : ObservableObject
	{
		private string? _Header;
		public string? Header { get => _Header; set => SetProperty(ref _Header, value); }

		private string? _Description;
		public string? Description { get => _Description; set => SetProperty(ref _Description, value); }

		private string? _Url;
		public string? Url { get => _Url; set => SetProperty(ref _Url, value); }

		private string? _DisplayUrl;
		public string? DisplayUrl { get => _DisplayUrl; set => SetProperty(ref _DisplayUrl, value); }

		private IconSource? _Icon;
		public IconSource? Icon { get => _Icon; set => SetProperty(ref _Icon, value); }

		// Out-dated
		private string? _UserLogin;
		public string? UserLogin { get => _UserLogin; set => SetProperty(ref _UserLogin, value); }

		// Out-dated
		private string? _RepositoryName;
		public string? RepositoryName { get => _RepositoryName; set => SetProperty(ref _RepositoryName, value); }

		private FrameNavigationParameter? _Context;
		public FrameNavigationParameter? Context { get => _Context; set => SetProperty(ref _Context, value); }
	}
}
