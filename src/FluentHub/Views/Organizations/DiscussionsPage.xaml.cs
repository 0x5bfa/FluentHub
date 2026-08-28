// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Organizations
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
