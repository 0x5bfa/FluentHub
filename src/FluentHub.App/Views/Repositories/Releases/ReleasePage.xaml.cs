using FluentHub.App.ViewModels.Repositories.Releases;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.Releases
{
	public sealed partial class ReleasePage : LocatablePage
	{
		public ReleaseViewModel ViewModel;

		public ReleasePage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Code)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<ReleaseViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryReleasePageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadRepositoryReleasePageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
