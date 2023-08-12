using FluentHub.App.Services;
using FluentHub.App.ViewModels.AppSettings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.Views.AppSettings
{
	public sealed partial class RepositoryPage : Page
	{
		public RepositoryPage()
		{
			InitializeComponent();

			navigationService = Ioc.Default.GetRequiredService<INavigationService>();
		}

		private readonly INavigationService navigationService;

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
			currentItem.Header = "Repositories settings";
			currentItem.Description = "Repositories settings";
			currentItem.Icon = new ImageIconSource
			{
				ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Repositories.png"))
			};
		}
	}
}
