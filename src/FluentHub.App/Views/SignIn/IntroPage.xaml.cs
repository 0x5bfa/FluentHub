// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.SignIn;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

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
			=> App.WindowInstance.SetTitleBar(AppTitleBar);

		private void OnGoToMainPageButtonWhenAuthorizedClick(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(Views.MainPage));
		}

		private void OnReportExceptionButtonClick(object sender, RoutedEventArgs e)
		{
		}

		private void OnSeeExceptionLogDetailsButtonClick(object sender, RoutedEventArgs e)
		{
		}
	}
}
