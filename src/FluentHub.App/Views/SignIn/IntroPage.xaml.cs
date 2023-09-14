// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.SignIn;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Windows.System;

namespace FluentHub.App.Views.SignIn
{
	public sealed partial class IntroPage : Page
	{
		public IntroPage()
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<IntroViewModel>();
		}

		public IntroViewModel ViewModel { get; }

		protected override void OnNavigatedTo(NavigationEventArgs e)
			=> MainWindow.Instance.SetTitleBar(AppTitleBar);

		private void OnGoToMainPageButtonWhenAuthorizedClick(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(Views.MainPage));
		}

		private async void OnReportExceptionButtonClick(object sender, RoutedEventArgs e)
		{
			// Load the URL in user's browser
			await Launcher.LaunchUriAsync(new Uri("https://github.com/FluentHub/FluentHub/issues/new?template=bug_report.yml"));
		}

		private async void OnSeeExceptionLogDetailsButtonClick(object sender, RoutedEventArgs e)
		{

			var dialog = new Dialogs.ExceptionStackTraceDialog(ViewModel.TaskException);

			// https://github.com/microsoft/microsoft-ui-xaml/issues/2504
			dialog.XamlRoot = this.Content.XamlRoot;

			_ = await dialog.ShowAsync();
		}
	}
}
