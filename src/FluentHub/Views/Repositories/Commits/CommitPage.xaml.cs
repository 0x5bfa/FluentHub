using FluentHub.Services;
using FluentHub.ViewModels.Repositories;
using FluentHub.ViewModels.Repositories.Commits;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.Data.Parameters;
using FluentHub.Octokit.Contracts;

namespace FluentHub.Views.Repositories.Commits
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
			if (e.Parameter is not FrameNavigationParameter { Parameters: Commit commit } param)
				return;

			ViewModel.Login = param.PrimaryText ?? string.Empty;
			ViewModel.Name = param.SecondaryText ?? string.Empty;
			ViewModel.CommitItem = commit;

			var command = ViewModel.LoadRepositoryCommitPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
