// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Services;
using FluentHub.ViewModels.Dialogs;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Dialogs
{
	public sealed partial class AccountSwitching : ContentDialog
	{
		public AccountSwitching()
			=> InitializeComponent();

		private void OnCancelButtonClick(object sender, RoutedEventArgs e)
			=> Hide();
	}
}
