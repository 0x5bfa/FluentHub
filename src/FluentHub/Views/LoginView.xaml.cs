// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;

namespace FluentHub.Views
{
	public sealed partial class LoginView : UserControl
	{
		public LoginView()
		{
			var app = App.Current;
			ViewModel = new LoginViewModel(
				app.Authorization,
				app.Session,
				app.GitHub,
				app.Settings);

			InitializeComponent();
			Unloaded += (_, _) => ViewModel.CancelAuthorization();
		}

		public LoginViewModel ViewModel { get; }

		public event EventHandler? SignInCompleted;

		private void OnContinueButtonClick(object sender, RoutedEventArgs e)
			=> SignInCompleted?.Invoke(this, EventArgs.Empty);

		private void OnCopyDeviceCodeButtonClick(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(ViewModel.DeviceUserCode))
				return;

			var dataPackage = new DataPackage();
			dataPackage.SetText(ViewModel.DeviceUserCode);
			Clipboard.SetContent(dataPackage);
			Clipboard.Flush();
		}
	}
}
