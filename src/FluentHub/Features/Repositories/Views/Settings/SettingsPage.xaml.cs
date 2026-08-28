using FluentHub.Services;
using FluentHub.Features.Repositories.ViewModels.Discussions;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.Features.Repositories.Views.Settings
{
	public sealed partial class SettingsPage : ScreenView
	{
		public SettingsPage()
		{
			InitializeComponent();

			navigationService = GetRequiredService<INavigationService>();
		}

		private readonly INavigationService navigationService;

		protected override void OnActivated(AppRoute route)
		{
			var chrome = navigationService.TabView.SelectedItem?.Chrome;
			if (chrome is null)
				return;

			chrome.Header = "Settings";
			chrome.Description = "Settings";
			chrome.Icon = new ImageIconSource
			{
				ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Settings.png"))
			};
		}
	}
}
