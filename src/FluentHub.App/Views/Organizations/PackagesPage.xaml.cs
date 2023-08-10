// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.Views.Organizations
{
	public sealed partial class PackagesPage : LocatablePage
	{
		public PackagesPage()
			: base(NavigationPageKind.Organization, NavigationPageKey.Packages)
		{
			InitializeComponent();
		}
	}
}
