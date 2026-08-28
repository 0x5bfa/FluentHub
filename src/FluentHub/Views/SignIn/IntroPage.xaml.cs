// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.ViewModels.SignIn;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;

namespace FluentHub.Views.SignIn
{
	public sealed partial class IntroPage : ScreenView
	{
		public IntroPage()
		{
			InitializeComponent();

			ViewModel = GetRequiredService<IntroViewModel>();
			Unloaded += (_, _) => ViewModel.CancelAuthorization();
		}

		public IntroViewModel ViewModel { get; }

		protected override void OnActivated(AppRoute route)
			=> App.Current.SignInWindow?.SetTitleBar(AppTitleBar);

		private void OnContinueButtonClick(object sender, RoutedEventArgs e)
			=> App.Current.CompleteSignIn();

		private void OnCopyDeviceCodeButtonClick(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(ViewModel.DeviceUserCode))
				return;

			var dataPackage = new DataPackage();
			dataPackage.SetText(ViewModel.DeviceUserCode);
			Clipboard.SetContent(dataPackage);
		}
	}
}
