// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Services;
using FluentHub.ViewModels.Dialogs;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Dialogs
{
	public sealed partial class UserProfileEditor : ContentDialog
	{
		public UserProfileEditor(string? login = null)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<EditUserProfileViewModel>();

			ViewModel.Login = login ?? string.Empty;
		}

		public EditUserProfileViewModel ViewModel { get; }

		private async void OnContentDialogLoaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
		{
			await ViewModel.LoadUserAsync(ViewModel.Login);
		}

		private async void OnContentDialogPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
		{
			await ViewModel.UpdateUserAsync(ViewModel.Login);
		}
	}
}
