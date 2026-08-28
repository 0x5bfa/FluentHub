// Copyright (c) 2022-2024 0x5BFA
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
