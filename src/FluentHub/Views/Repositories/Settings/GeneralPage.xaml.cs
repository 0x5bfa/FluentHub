using FluentHub.Services;
using FluentHub.ViewModels.Repositories.Discussions;
using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Views.Repositories.Settings
{
	public sealed partial class GeneralPage : Page
	{
		public GeneralPage()
		{
			this.InitializeComponent();
			navigationService = Ioc.Default.GetRequiredService<INavigationService>();
		}

		private readonly INavigationService navigationService;

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			if (e.Parameter is not string url || !Uri.TryCreate(url, UriKind.Absolute, out var uri))
				return;

			var pathSegments = uri.AbsolutePath.Split("/").ToList();
			pathSegments.RemoveAt(0);

			var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
			if (currentItem is null)
				return;

			currentItem.Header = "Settings";
			currentItem.Description = "Settings";

			currentItem.Icon = new ImageIconSource
			{
				ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Settings.png"))
			};
		}
	}
}
