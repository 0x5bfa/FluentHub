// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.Views.Organizations
{
	public sealed partial class MembershipPage : LocatablePage
	{
		public MembershipPage()
			: base(NavigationPageKind.Organization, NavigationPageKey.People)
		{
			InitializeComponent();
		}
	}
}
