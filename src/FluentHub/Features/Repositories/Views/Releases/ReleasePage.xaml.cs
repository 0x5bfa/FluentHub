using FluentHub.Services;
using FluentHub.Features.Repositories.ViewModels.Releases;

namespace FluentHub.Features.Repositories.Views.Releases
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
