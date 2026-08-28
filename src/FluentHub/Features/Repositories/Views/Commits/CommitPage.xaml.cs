using FluentHub.Services;
using FluentHub.Features.Repositories.ViewModels;
using FluentHub.Features.Repositories.ViewModels.Commits;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.Core.Application.Models;

namespace FluentHub.Features.Repositories.Views.Commits
{
	public sealed partial class CommitPage : NavigableView
	{
		public CommitViewModel ViewModel { get; }

		public CommitPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Code)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<CommitViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryCommitPageCommand;
			_screenViewModel = ViewModel;
		}

		protected override void OnActivated(AppRoute route)
		{
			if (route is not RepositoryCommitRoute commit)
				return;

			ViewModel.CommitItem = new Commit { Oid = commit.Sha };

			var command = ViewModel.LoadRepositoryCommitPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
