// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.App.Views.Organizations
{
	public sealed partial class DiscussionsPage : LocatablePage
	{
		public DiscussionsPage()
			: base(NavigationPageKind.Organization, NavigationPageKey.Discussions)
		{
			InitializeComponent();
		}
	}
}
