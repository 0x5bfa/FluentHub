// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Organizations
{
	public sealed partial class PackagesPage : NavigableView
	{
		public PackagesPage()
			: base(NavigationPageKind.Organization, NavigationPageKey.Packages)
		{
			InitializeComponent();
		}
	}
}
