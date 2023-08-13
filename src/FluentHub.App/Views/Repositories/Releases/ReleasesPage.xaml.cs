using FluentHub.App.Helpers;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories.Releases;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.Releases
{
	public sealed partial class ReleasesPage : LocatablePage
	{
		public ReleasesViewModel ViewModel;

		public ReleasesPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Code)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<ReleasesViewModel>();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadRepositoryReleasesPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
