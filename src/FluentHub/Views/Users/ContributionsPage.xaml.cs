// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Users
{
	public sealed partial class ContributionsPage : NavigableView
	{
		public ContributionsPage() : base(NavigationPageKind.User, NavigationPageKey.None)
		{
			InitializeComponent();
		}
	}
}
