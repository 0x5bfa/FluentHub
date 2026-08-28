// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Services;
using FluentHub.ViewModels.Dialogs;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Dialogs
{
	public sealed partial class EditPinnedRepositoriesDialog : ContentDialog
	{
		public EditPinnedRepositoriesDialog(string? login = null)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<EditPinnedRepositoriesDialogViewModel>();

			ViewModel.Login = login ?? string.Empty;
		}

		public EditPinnedRepositoriesDialogViewModel ViewModel { get; }

		private void OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
		{
		}

		private async void OnEditPinnedRepositoriesDialogLoaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
		{
			await ViewModel.LoadPinnableAndPinnedRepositoriesAsync();
		}
	}
}
