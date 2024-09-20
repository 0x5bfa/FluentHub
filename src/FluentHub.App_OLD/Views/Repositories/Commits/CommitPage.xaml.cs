using FluentHub.App.ViewModels.Repositories.Commits;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.Commits
{
	public sealed partial class CommitPage : LocatablePage
	{
		public CommitViewModel ViewModel { get; }

		public CommitPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Code)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<CommitViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryCommitPageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var param = e.Parameter as FrameNavigationParameter;
			ViewModel.Login = param.PrimaryText;
			ViewModel.Name = param.SecondaryText;
			ViewModel.CommitItem = param.Parameters as Commit;

			var command = ViewModel.LoadRepositoryCommitPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
