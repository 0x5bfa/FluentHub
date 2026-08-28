// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Features.Organizations.Views
{
	public sealed partial class DiscussionsPage : NavigableView
	{
		public DiscussionsPage()
			: base(NavigationPageKind.Organization, NavigationPageKey.Discussions)
		{
			InitializeComponent();
		}
	}
}
