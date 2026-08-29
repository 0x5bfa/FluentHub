using FluentHub.Services;
using FluentHub.ViewModels.Repositories.Releases;

namespace FluentHub.Views.Repositories.Releases
{
	public sealed partial class ReleasePage : NavigableView
	{
		public ReleaseViewModel ViewModel;

		public ReleasePage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Code)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<ReleaseViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryReleasePageCommand;
			_screenViewModel = ViewModel;
		}

		protected override void OnActivated(AppRoute route)
		{
			var command = ViewModel.LoadRepositoryReleasePageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
