// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.ComponentModel;
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
			ViewModel.PropertyChanged += OnViewModelPropertyChanged;
		}

		public LoginViewModel ViewModel { get; }

		public event EventHandler? SignInCompleted;

		private bool _authorizationStarted;
		private bool _signInCompleted;

		public async Task StartAuthorizationAsync()
		{
			if (_authorizationStarted)
			{
				return;
			}

			_authorizationStarted = true;
			await ViewModel.StartAuthorizationAsync();
		}

		public void StopAuthorization()
		{
			ViewModel.CancelAuthorization();
			ViewModel.PropertyChanged -= OnViewModelPropertyChanged;
		}

		private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs args)
		{
			if (_signInCompleted ||
				args.PropertyName != nameof(LoginViewModel.Stage) ||
				ViewModel.Stage != LoginStage.Success)
			{
				return;
			}

			_signInCompleted = true;
			SignInCompleted?.Invoke(this, EventArgs.Empty);
		}

		private void OnCopyDeviceCodeButtonClick(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(ViewModel.DeviceUserCode))
			{
				return;
			}

			var dataPackage = new DataPackage();
			dataPackage.SetText(ViewModel.DeviceUserCode);
			Clipboard.SetContent(dataPackage);
			Clipboard.Flush();
		}
	}
}
