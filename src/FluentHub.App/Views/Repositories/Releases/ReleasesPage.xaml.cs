using FluentHub.App.Helpers;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories.Releases;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using FluentHub.App.Data.Parameters;

namespace FluentHub.App.Views.Repositories.Releases
{
	public sealed partial class ReleasesPage : LocatablePage
	{
		public ReleasesViewModel ViewModel;

		private readonly INavigationService _navigation;

		public ReleasesPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Code)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<ReleasesViewModel>();
			_navigation = Ioc.Default.GetRequiredService<INavigationService>();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var param = e.Parameter as FrameNavigationParameter;
			_ = param ?? throw new ArgumentNullException("param");

			ViewModel.Login = param.PrimaryText;
			ViewModel.Name = param.SecondaryText;

			var command = ViewModel.LoadRepositoryReleasesPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private void OnReleaseBlockButtonClick(object sender, RoutedEventArgs e)
		{
			_navigation.Navigate<ReleasePage>(
				new FrameNavigationParameter()
				{
					PrimaryText = ViewModel.Repository.Owner.Login,
					SecondaryText = ViewModel.Repository.Name,
					Parameters = new() { $"{(sender as Button).Tag as string}" }
				});
		}
	}
}
