using FluentHub.Services;
using FluentHub.Features.Repositories.ViewModels;
using FluentHub.Features.Repositories.ViewModels.Commits;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.Features.Repositories.Views.Commits
{
	public sealed partial class CommitsPage : NavigableView
	{
		public CommitsViewModel ViewModel { get; }
		private readonly INavigationService _navigation;

		public CommitsPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Code)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<CommitsViewModel>();
			_navigation = GetRequiredService<INavigationService>();
			_pageLoadCommand = ViewModel.LoadRepositoryCommitsPageCommand;
			_screenViewModel = ViewModel;
		}

		protected override void OnActivated(AppRoute route)
		{
			if (route is not RepositoryCommitsRoute commits)
				return;

			ViewModel.ContextViewModel = new RepoContextViewModel
			{
				BranchName = commits.GitRef ?? string.Empty,
				Path = commits.Path ?? string.Empty,
			};

			var command = ViewModel.LoadRepositoryCommitsPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
			{
				var command = ViewModel.LoadRepositoryCommitsFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
